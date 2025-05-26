using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetQueStore.exe201.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetQueStore.exe201.Pages
{
    public class SpecialtiesModel : PageModel
    {
        private readonly Exe2Context _context;
        public Dictionary<int, (string Name, int ProductCount)> CategoriesWithCount { get; set; } = new();

        public SpecialtiesModel(Exe2Context context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        [FromQuery(Name = "page")]
        public int CurrentPage { get; set; } = 1;


        [BindProperty(SupportsGet = true)]
        public decimal MinPrice { get; set; } = 0;

        [BindProperty(SupportsGet = true)]
        public decimal MaxPrice { get; set; } = 1000000;

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; } = "default";

        [BindProperty(SupportsGet = true)]
        public int? CategoryId { get; set; }
        public List<Category> Categories { get; set; } = new();
        public List<Food>? Foods { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; } = 12;


        public async Task OnGetAsync()
        {

            if (MaxPrice == 0)
            {
                MaxPrice = await _context.Foods
                    .Where(f => f.IsActive)
                    .MaxAsync(f => f.Price);
            }

            var query = _context.Foods
                .Where(f => f.IsActive && f.Price >= MinPrice && f.Price <= MaxPrice);

            if (CategoryId.HasValue)
            {
                query = query.Where(f => f.CategoryId == CategoryId.Value);
            }

            switch (SortOrder.ToLower())
            {
                case "price_asc":
                    query = query.OrderBy(f => f.Price);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(f => f.Price);
                    break;
                default:
                    query = query.OrderBy(f => f.Id);
                    break;
            }

            TotalItems = await query.CountAsync();

            Foods = await query
                .Include(f => f.FoodImages)
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            CategoriesWithCount = await _context.Categories
                .Where(c => c.IsActive)
                .OrderBy(c => c.DisplayOrder)
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    ProductCount = c.Foods.Count(f => f.IsActive)
                })
                .ToDictionaryAsync(c => c.Id, c => (c.Name, c.ProductCount));
        }

    }
}

