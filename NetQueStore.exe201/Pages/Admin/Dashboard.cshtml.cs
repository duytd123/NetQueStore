using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetQueStore.exe201.Models;

namespace NetQueStore.exe201.Pages.Admin
{
    public class Dashboard : PageModel
    {
        private readonly DbexeContext _context;

        public Dashboard(DbexeContext context)
        {
            _context = context;
        }
        public List<Food> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Region> Regions { get; set; }
        public List<Province> Provinces { get; set; }
        public async Task OnGetAsync()
        {
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
                new { Title = "Tổng sản phẩm", Value = Products.Count.ToString(), Icon = "bi-box", Bg = "primary" },
                new { Title = "Loại hàng", Value = Categories.Count.ToString(), Icon = "bi-cart-check", Bg = "success" },
                new { Title = "Vùng", Value = Regions.Count.ToString(), Icon = "bi-shop", Bg = "warning" },
                new { Title = "Khu vực", Value = Provinces.Count.ToString(), Icon = "bi-currency-dollar", Bg = "danger" }
            };
            var categoryData = Products
                .GroupBy(p => p.Category?.Name ?? "Không rõ")
                .Select(g => new { Label = g.Key, Count = g.Count() })
                .ToList();

                    ViewData["ChartData"] = categoryData;
        }


    }
}
