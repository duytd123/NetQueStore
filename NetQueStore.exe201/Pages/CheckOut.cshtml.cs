using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetQueStore.exe201.Models;
using NetQueStore.exe201.Models.PayOs;
using NetQueStore.exe201.Services.Payos;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Threading.Tasks;

namespace NetQueStore.exe201.Pages;

public class CheckOutModel : PageModel
{
    private readonly Exe2Context _context;
    private readonly PayOSService _payOSService;
    private readonly ILogger<CheckOutModel> _logger;

    public CheckOutModel(Exe2Context context, PayOSService payOSService, ILogger<CheckOutModel> logger)
    {
        _context = context;
        _payOSService = payOSService;
        _logger = logger;
    }

    public List<Food> ListOrderItems { get; set; } = new();
    public Dictionary<int, int> FoodQuantities { get; set; } = new();
    public List<PaymentMethod> PaymentMethods { get; set; } = new();

    [Required(ErrorMessage = "Họ tên là bắt buộc")]
    [BindProperty]
    public string FullName { get; set; } = "";

    [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
    [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
    [BindProperty]
    public string Phone { get; set; } = "";

    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    [BindProperty]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
    [BindProperty]
    public string Address { get; set; } = "";

    [BindProperty]
    public string Notes { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán")]
    [BindProperty]
    public int PaymentMethodId { get; set; }

    public decimal Subtotal { get; set; }
    public decimal ShippingFee { get; set; }
    public decimal Total { get; set; }

    public void OnGet()
    {
        _logger.LogInformation("OnGet called at {Time}", DateTime.Now);
        var cart = LoadCartData();
        ApplyCart(cart);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        _logger.LogInformation("OnPostAsync called with FullName: {FullName}, Phone: {Phone}, Email: {Email}, Address: {Address}, PaymentMethodId: {PaymentMethodId}",
            FullName, Phone, Email, Address, PaymentMethodId);

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("ModelState invalid: {Errors}", JsonSerializer.Serialize(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
            var cart = LoadCartData();
            ApplyCart(cart);
            return new JsonResult(new { success = false, message = "Dữ liệu không hợp lệ, vui lòng kiểm tra lại." });
        }

        var cartData = LoadCartData();
        ApplyCart(cartData);

        if (cartData.FoodQuantities.Count == 0)
        {
            _logger.LogWarning("Cart is empty");
            return new JsonResult(new { success = false, message = "Giỏ hàng của bạn đang trống." });
        }

        foreach (var kv in cartData.FoodQuantities)
        {
            var foodId = kv.Key;
            var requestedQty = kv.Value;

            var food = await _context.Foods.FirstOrDefaultAsync(f => f.Id == foodId && f.IsActive);
            if (food == null)
            {
                _logger.LogWarning("Product not found or inactive: ID {FoodId}", foodId);
                return new JsonResult(new { success = false, message = $"Sản phẩm với ID {foodId} không tồn tại." });
            }

            if (requestedQty > food.StockQuantity)
            {
                _logger.LogWarning("Requested quantity {RequestedQty} exceeds stock {StockQuantity} for product: {ProductName}", requestedQty, food.StockQuantity, food.Name);
                return new JsonResult(new { success = false, message = $"Sản phẩm \"{food.Name}\" chỉ còn lại {food.StockQuantity} trong kho." });
            }
        }

        var selectedMethod = await _context.PaymentMethods.FirstOrDefaultAsync(p => p.Id == PaymentMethodId && p.IsActive);
        if (selectedMethod == null)
        {
            _logger.LogWarning("Invalid payment method selected: ID {PaymentMethodId}", PaymentMethodId);
            return new JsonResult(new { success = false, message = "Vui lòng chọn phương thức thanh toán hợp lệ." });
        }

        try
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            _logger.LogInformation("Started database transaction for order creation");

            int? userId = HttpContext.Session.GetInt32("id");
            string? sessionId = HttpContext.Session.GetString("session_id");
            string? guestEmail = HttpContext.Session.GetString("guest_email");
            _logger.LogInformation("Session data: UserId={UserId}, SessionId={SessionId}, GuestEmail={GuestEmail}", userId, sessionId, guestEmail);

            var order = new NetQueStore.exe201.Models.Order

            {
                OrderNumber = GenerateOrderNumber(),
                BankId = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                UserId = userId,
                SessionId = sessionId,
                GuestEmail = guestEmail,
                RecipientName = FullName,
                RecipientPhone = Phone,
                RecipientEmail = Email,
                DeliveryAddress = Address,
                DeliveryNotes = Notes,
                PaymentMethodId = selectedMethod.Id,
                ShippingFee = cartData.ShippingFee,
                Subtotal = cartData.Subtotal,
                TotalAmount = cartData.Total,
                OrderDate = DateTime.Now,
                OrderStatus = "created",
                PaymentStatus = "pending",
                CreatedAt = DateTime.Now
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Order created with ID: {OrderId}, OrderNumber: {OrderNumber}", order.Id, order.OrderNumber);

            foreach (var kv in cartData.FoodQuantities)
            {
                var food = await _context.Foods.FindAsync(kv.Key);
                if (food == null) continue;

                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    FoodId = food.Id,
                    ProductName = food.Name,
                    Quantity = kv.Value,
                    UnitPrice = food.Price,
                    Subtotal = food.Price * kv.Value,
                    CreatedAt = DateTime.Now
                };
                _context.OrderItems.Add(orderItem);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            _logger.LogInformation("Transaction committed for order ID: {OrderId}", order.Id);

            Response.Cookies.Delete("shopping_cart");

            if (selectedMethod.Code == "card")
            {
                var returnUrl = $"{Request.Scheme}://{Request.Host}/Checkout/PaymentCallbackPayos";
                var cancelUrl = $"{Request.Scheme}://{Request.Host}/Cart";
                _logger.LogInformation("Creating PayOS payment with returnUrl: {ReturnUrl}, cancelUrl: {CancelUrl}", returnUrl, cancelUrl);

                var payReq = new PayOSPaymentRequest
                {
                    Amount = (int)order.TotalAmount,
                    Description = $"TT{order.OrderNumber}",
                    OrderCode = order.BankId?.ToString() ?? "0",
                    ReturnUrl = returnUrl,
                    CancelUrl = cancelUrl
                };

                _logger.LogInformation("PayOS request: {Request}", JsonSerializer.Serialize(payReq));

                var paymentResult = await _payOSService.CreatePaymentAsync(payReq);

                if (paymentResult == null || string.IsNullOrEmpty(paymentResult.checkoutUrl))
                {
                    _logger.LogError("Failed to create PayOS payment link for order {OrderNumber}. Amount: {Amount}, OrderCode: {OrderCode}",
                        order.OrderNumber, payReq.Amount, payReq.OrderCode);
                    return new JsonResult(new { success = false, message = "Không thể tạo link thanh toán. Vui lòng thử lại." });
                }

                _logger.LogInformation("PayOS payment link created: {CheckoutUrl}", paymentResult.checkoutUrl);
                return new JsonResult(new { success = true, redirectUrl = paymentResult.checkoutUrl });
            }

            _logger.LogInformation("Order completed without payment gateway for order ID: {OrderId}", order.Id);
            return new JsonResult(new { success = true, redirectUrl = Url.Page("/OrderComplete", new { id = order.Id }) });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing order: {Message}", ex.Message);
            return new JsonResult(new { success = false, message = "Lỗi khi đặt hàng: " + (ex.InnerException?.Message ?? ex.Message) });
        }
    }

    private class CartData
    {
        public Dictionary<int, int> FoodQuantities { get; set; } = new();
        public List<Food> ListOrderItems { get; set; } = new();
        public decimal Subtotal { get; set; }
        public decimal ShippingFee { get; set; }
        public decimal Total { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; } = new();
    }

    private CartData LoadCartData()
    {
        var cart = new CartData();
        cart.FoodQuantities = GetCartFromCookie();

        cart.ListOrderItems = _context.Foods
            .Include(f => f.FoodImages)
            .Where(f => cart.FoodQuantities.Keys.Contains(f.Id) && f.IsActive)
            .ToList();

        cart.Subtotal = cart.ListOrderItems.Sum(f => f.Price * cart.FoodQuantities[f.Id]);

        int provinceId = 1;
        var provinceIdStr = HttpContext.Session.GetString("province_id");
        if (!string.IsNullOrEmpty(provinceIdStr) && int.TryParse(provinceIdStr, out int pId))
            provinceId = pId;

        var shippingData = _context.ShippingFees
            .Where(sf => sf.ProvinceId == provinceId)
            .OrderByDescending(sf => sf.CreatedAt)
            .FirstOrDefault();

        if (shippingData != null)
        {
            if (shippingData.MinOrderValue.HasValue && cart.Subtotal >= shippingData.MinOrderValue.Value)
            {
                cart.ShippingFee = 0;
            }
            else
            {
                cart.ShippingFee = shippingData.Fee;
            }
        }
        else
        {
            cart.ShippingFee = 10000;
        }

        cart.Total = cart.Subtotal + cart.ShippingFee;

        cart.PaymentMethods = _context.PaymentMethods
            .Where(pm => pm.IsActive)
            .OrderBy(pm => pm.DisplayOrder)
            .ToList();

        return cart;
    }

    private Dictionary<int, int> GetCartFromCookie()
    {
        var result = new Dictionary<int, int>();
        var cookie = Request.Cookies["shopping_cart"];
        if (!string.IsNullOrEmpty(cookie))
        {
            var ids = cookie.Split('-', StringSplitOptions.RemoveEmptyEntries);
            foreach (var idStr in ids)
            {
                if (int.TryParse(idStr, out int id))
                {
                    if (result.ContainsKey(id))
                        result[id]++;
                    else
                        result[id] = 1;
                }
            }
        }
        return result;
    }

    private void ApplyCart(CartData cart)
    {
        FoodQuantities = cart.FoodQuantities;
        ListOrderItems = cart.ListOrderItems;
        Subtotal = cart.Subtotal;
        ShippingFee = cart.ShippingFee;
        Total = cart.Total;
        PaymentMethods = cart.PaymentMethods;
    }

    private string GenerateOrderNumber()
    {
        return $"ORD{DateTime.Now:yyyyMMddHHmmssfff}";
    }
}