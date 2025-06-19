using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetQueStore.exe201.Models;

namespace NetQueStore.exe201.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        private readonly Exe2Context _context;

        public DashboardModel(Exe2Context context)
        {
            _context = context;
        }
        public List<Food> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Region> Regions { get; set; }
        public List<Province> Provinces { get; set; }
        public List<Models.Order> LatestOrders { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var isLoggedIn = HttpContext.Session.GetString("AdminLoggedIn");
            if (string.IsNullOrEmpty(isLoggedIn))
            {
                return RedirectToPage("/Admin/Login");
            }

            Products = await _context.Foods
                .Include(f => f.Category)
                .Include(f => f.Region)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();

            Categories = await _context.Categories.ToListAsync();
            Regions = await _context.Regions.ToListAsync();
            Provinces = await _context.Provinces.ToListAsync();

            ViewData["Status"] = new List<dynamic>
    {
        new { Title = "Tổng sản phẩm hiện có", Value = Products.Count.ToString(), Icon = "bi-box", Bg = "primary" },
        new { Title = "Loại hàng", Value = Categories.Count.ToString(), Icon = "bi-cart-check", Bg = "success" },
        new { Title = "Vùng", Value = Regions.Count.ToString(), Icon = "bi-shop", Bg = "warning" },
        new { Title = "Tỉnh", Value = Provinces.Count.ToString(), Icon = "bi-city", Bg = "danger" }
    };

            // Dữ liệu biểu đồ
            var categoryData = Products
                .GroupBy(p => p.Category?.Name ?? "Không rõ")
                .Select(g => new { Label = g.Key, Count = g.Count() })
                .ToList();

            ViewData["ChartData"] = categoryData;

            LatestOrders = await _context.Orders
        .OrderByDescending(o => o.OrderDate)
        .Take(5)
        .ToListAsync();


            return Page();
        }


        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }

    }
}
