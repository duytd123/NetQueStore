using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetQueStore.exe201.Models;

namespace NetQueStore.exe201.Pages
{
    public class DetailModel : PageModel
    {
        private readonly Exe2Context _context;

        public DetailModel(Exe2Context context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public Food? Food { get; set; }

        public List<Category> Categories { get; set; } = new();
        public List<Food> FeaturedFoods { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            if (Id <= 0) return BadRequest();

            Food = await _context.Foods
                .Include(f => f.Category)
                .Include(f => f.Province)
                .Include(f => f.FoodImages)
                .FirstOrDefaultAsync(f => f.Id == Id);

            if (Food == null) return NotFound();

            // Lấy danh mục đang hoạt động
            Categories = await _context.Categories
                .Where(c => c.IsActive)
                .OrderBy(c => c.DisplayOrder)
                .ToListAsync();

            // Lấy các sản phẩm được đặt hàng nhiều nhất
            FeaturedFoods = await _context.OrderItems
                .Where(oi => oi.FoodId != null)
                .GroupBy(oi => oi.FoodId)
                .Select(g => new
                {
                    FoodId = g.Key,
                    TotalOrdered = g.Sum(x => x.Quantity ?? 0)
                })
                .OrderByDescending(x => x.TotalOrdered)
                .Take(5)
                .Join(_context.Foods.Include(f => f.FoodImages),
                    x => x.FoodId,
                    f => f.Id,
                    (x, f) => f)
                .ToListAsync();

            return Page();
        }
    }
}
