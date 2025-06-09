using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using NetQueStore.exe201.Models;
namespace NetQueStore.exe201.Pages
{
    public class CartModel : PageModel
    {
        private readonly Exe2Context _context;

        public List<PaymentMethod> PaymentMethods { get; set; } = new();

        public List<Food> ListOrderItems { get; set; } = new();
        public Dictionary<int, int> FoodQuantities { get; set; } = new();

        public decimal Subtotal { get; set; } = 0;
        public decimal ShippingFee { get; set; }   
        public decimal Total { get; set; } = 0;
        public int Step { get; set; } = 1;

        public string errorMessage = "";
        public string successMessage = "";

        public CartModel(Exe2Context context)
        {
            _context = context;
        }

        private Dictionary<int, int> GetFoodDictionary()
        {
            var dict = new Dictionary<int, int>();

            string cookieValue = Request.Cookies["shopping_cart"] ?? "";
            if (!string.IsNullOrEmpty(cookieValue))
            {
                var ids = cookieValue.Split('-', StringSplitOptions.RemoveEmptyEntries);
                foreach (var idStr in ids)
                {
                    if (int.TryParse(idStr, out int id))
                    {
                        if (dict.ContainsKey(id))
                            dict[id]++;
                        else
                            dict[id] = 1;
                    }
                }
            }

            return dict;
        }

        public void OnGet()
        {
            var foodDict = GetFoodDictionary();

            // Xử lý action thêm bớt xóa món ăn trong cookie
            string? action = Request.Query["action"];
            string? idStr = Request.Query["id"];

            if (!string.IsNullOrEmpty(action) && !string.IsNullOrEmpty(idStr) && int.TryParse(idStr, out int id) && foodDict.ContainsKey(id))
            {
                switch (action)
                {
                    case "add":
                        foodDict[id]++;
                        break;
                    case "sub":
                        if (foodDict[id] > 1) foodDict[id]--;
                        break;
                    case "delete":
                        foodDict.Remove(id);
                        break;
                }

                // Cập nhật lại cookie
                string newCookieValue = string.Join("-", foodDict.Select(kv => Enumerable.Repeat(kv.Key.ToString(), kv.Value)).SelectMany(x => x));
                var cookieOptions = new Microsoft.AspNetCore.Http.CookieOptions
                {
                    Expires = DateTime.Now.AddDays(365),
                    Path = "/"
                };
                Response.Cookies.Append("shopping_cart", newCookieValue, cookieOptions);

                // Redirect để tránh query string action
                Response.Redirect(Request.Path);
                return;
            }

            if (foodDict.Count == 0)
            {
                ListOrderItems = new List<Food>();
                FoodQuantities = new Dictionary<int, int>();
                Subtotal = 0;
                Total = 0;
                return;
            }

            PaymentMethods = _context.PaymentMethods
       .Where(pm => pm.IsActive)
       .OrderBy(pm => pm.DisplayOrder)
       .ToList();


            // Lấy dữ liệu món ăn theo danh sách id trong giỏ
            ListOrderItems = _context.Foods
                .Include(f => f.FoodImages)
                .Include(f => f.Category)
                .Where(f => foodDict.Keys.Contains(f.Id) && f.IsActive)
                .ToList();

            FoodQuantities = foodDict;

            Subtotal = 0;
            foreach (var food in ListOrderItems)
            {
                if (FoodQuantities.TryGetValue(food.Id, out int qty))
                {
                    Subtotal += food.Price * qty;
                }
            }

            // Lấy provinceId từ Session hoặc mặc định
            int provinceId = 1; // default provinceId
            var provinceIdStr = HttpContext.Session.GetString("province_id");
            if (!string.IsNullOrEmpty(provinceIdStr) && int.TryParse(provinceIdStr, out int pId))
            {
                provinceId = pId;
            }

            // Lấy phí ship theo provinceId từ DB
            var shippingFeeEntity = _context.ShippingFees
                .Where(sf => sf.ProvinceId == provinceId)
                .OrderByDescending(sf => sf.CreatedAt)
                .FirstOrDefault();

            ShippingFee = shippingFeeEntity?.Fee ?? 10000; 

            Total = Subtotal + ShippingFee;

        }

        public int GetQuantity(int foodId)
        {
            return FoodQuantities.TryGetValue(foodId, out int qty) ? qty : 0;
        }

        public IActionResult OnPost()
        {
            int? clientId = HttpContext.Session.GetInt32("id");
            string? sessionId = HttpContext.Session.GetString("session_id");
            string? guestEmail = HttpContext.Session.GetString("guest_email");

            if (clientId == null && (string.IsNullOrEmpty(sessionId) || string.IsNullOrEmpty(guestEmail)))
            {
                return Redirect("/CheckOut"); 
            }

            if (!ModelState.IsValid)
            {
                errorMessage = "Please fill in all required fields.";
                OnGet();  
                return Page();
            }

            // Optional: kiểm tra giỏ hàng rỗng
            var foodDict = GetFoodDictionary();
            if (foodDict.Count == 0)
            {
                errorMessage = "Giỏ hàng của bạn đang trống.";
                OnGet();
                return Page();
            }

            return RedirectToPage("/Checkout");

        }

    }
}

