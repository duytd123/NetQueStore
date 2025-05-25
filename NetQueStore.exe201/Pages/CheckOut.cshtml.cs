using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetQueStore.exe201.Models;
using NetQueStore.exe201.Models.PayOs;
using NetQueStore.exe201.Models.Vnpay;
using NetQueStore.exe201.Services.Payos;
using NetQueStore.exe201.Services.Vnpay;
using System.ComponentModel.DataAnnotations;

namespace NetQueStore.exe201.Pages
{
    public class CheckOutModel : PageModel
    {
        private readonly Exe2Context _context;
        private readonly IVnPayService _vnPayService;
        private readonly PayOSService _payOSService;

        public CheckOutModel(Exe2Context context, IVnPayService vnPayService, PayOSService payOSService )
        {
            _context = context;
            _vnPayService = vnPayService;
            _payOSService = payOSService;
            
        }

        public List<Food> ListOrderItems { get; set; } = new();
        public Dictionary<int, int> FoodQuantities { get; set; } = new();
        public List<PaymentMethod> PaymentMethods { get; set; } = new();

        [Required]
        [BindProperty]
        public string? FullName { get; set; } = "";

        [Required]
        [BindProperty]
        public string Phone { get; set; } = "";

        [EmailAddress]
        [BindProperty]
        public string Email { get; set; } = "";

        [Required]
        [BindProperty]
        public string Address { get; set; } = "";

        [BindProperty]
        public string? Notes { get; set; }

        [Required]
        [BindProperty]
        public int PaymentMethodId { get; set; }

        // Price summary
        public decimal Subtotal { get; set; }
        public decimal ShippingFee { get; set; }
        public decimal Total { get; set; }

        public void OnGet()
        {
            var cart = LoadCartData();
            ApplyCart(cart);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var cart = LoadCartData();
                ApplyCart(cart);
                return Page();
            }

            var cartData = LoadCartData();
            ApplyCart(cartData);

            if (cartData.FoodQuantities.Count == 0)
            {
                ModelState.AddModelError("", "Giỏ hàng của bạn đang trống.");
                return Page();
            }

            var selectedMethod = _context.PaymentMethods
                .FirstOrDefault(p => p.Id == PaymentMethodId && p.IsActive);

            if (selectedMethod == null)
            {
                ModelState.AddModelError("PaymentMethodId", "Vui lòng chọn phương thức thanh toán hợp lệ.");
                return Page();
            }

            try
            {
                using var transaction = _context.Database.BeginTransaction();

                int? userId = HttpContext.Session.GetInt32("id");
                string? sessionId = HttpContext.Session.GetString("session_id");
                string? guestEmail = HttpContext.Session.GetString("guest_email");

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
                _context.SaveChanges();

                foreach (var kv in cartData.FoodQuantities)
                {
                    var food = _context.Foods.Find(kv.Key);
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

                _context.SaveChanges();
                transaction.Commit();

                Response.Cookies.Delete("shopping_cart");

                if (selectedMethod.Code == "card")
                {
                    var returnUrl = $"{Request.Scheme}://{Request.Host}/Checkout/PaymentCallbackPayos";
                    var cancelUrl = $"{Request.Scheme}://{Request.Host}/Cart";

                    var payReq = new PayOSPaymentRequest
                    {
                        Amount = (int)order.TotalAmount,
                        Description = $"TT{order.OrderNumber}",
                        OrderCode = order.BankId?.ToString() ?? "0",
                        ReturnUrl = returnUrl,
                        CancelUrl = cancelUrl
                    };

                    var paymentResult = await _payOSService.CreatePaymentAsync(payReq);

                    if (paymentResult == null || string.IsNullOrEmpty(paymentResult.checkoutUrl))
                    {
                        var errorMessage = $"Không thể tạo link thanh toán cho đơn hàng {order.OrderNumber} - Amount: {payReq.Amount}, OrderCode: {payReq.OrderCode}";
                        TempData["ErrorMessage"] = errorMessage;
                        return RedirectToPage("/Error", new { message = errorMessage });
                    }

                    return Redirect(paymentResult.checkoutUrl);
                }


                TempData["SuccessMessage"] = "Đặt hàng thành công!";
                return RedirectToPage("/OrderComplete", new { id = order.Id });
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.Message ?? ex.Message;
                TempData["ErrorMessage"] = "Lỗi khi đặt hàng: " + inner;
                ModelState.AddModelError("", "Lỗi khi đặt hàng: " + inner);

                return Page();
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

            cart.ShippingFee = _context.ShippingFees
                .Where(sf => sf.ProvinceId == provinceId)
                .OrderByDescending(sf => sf.CreatedAt)
                .Select(sf => sf.Fee)
                .FirstOrDefault();

            if (cart.ShippingFee == 0) cart.ShippingFee = 10000;

            cart.Total = cart.Subtotal + cart.ShippingFee;

            cart.PaymentMethods = _context.PaymentMethods
                .Where(pm => pm.IsActive)
                .OrderBy(pm => pm.DisplayOrder)
                .ToList();

            return cart;
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

        private string GenerateOrderNumber()
        {
            return $"ORD{DateTime.Now:yyyyMMddHHmmssfff}";
        }
    }
}
