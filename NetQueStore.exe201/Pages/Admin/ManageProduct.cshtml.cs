using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetQueStore.exe201.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetQueStore.exe201.Pages.Admin
{
    public class ManageProduct : PageModel
    {
        private readonly Exe2Context _context;
        private readonly IWebHostEnvironment _env;
        public ManageProduct(Exe2Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public List<Food> Products { get; set; }
        //public List<Order> Orders { get; set; }
        public List<Category> Categories { get; set; }
        public List<Region> Regions { get; set; }
        public List<Province> Provinces { get; set; }


        public async Task OnGetAsync()
        {
            Products = await _context.Foods
                .Include(f => f.Category)
                .Include(f => f.Region)
                .Include(f => f.FoodImages)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();

        

            Categories = await _context.Categories.ToListAsync();
            Regions = await _context.Regions.ToListAsync();
            Provinces = await _context.Provinces.ToListAsync();
        }

        public async Task<IActionResult> OnPostAddProductAsync(Food product, List<IFormFile> Images)
        {

            try
            {
                product.Slug = product.Name.ToLower().Replace(" ", "-");
                product.IsActive = true;
                product.CreatedAt = DateTime.UtcNow;

                _context.Foods.Add(product);
                await _context.SaveChangesAsync();

                foreach (var image in Images)
                {
                    if (image.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        var filePath = Path.Combine(_env.WebRootPath, "uploads", fileName);

                        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }

                        var foodImage = new FoodImage()
                        {
                            FoodId = product.Id,
                            Filename = "/uploads/" + fileName,
                            CreatedAt = DateTime.UtcNow
                        };

                        _context.FoodImages.Add(foodImage);
                    }
                }

                await _context.SaveChangesAsync();

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "L?i khi l?u d? li?u: " + ex.Message);
                Products = await _context.Foods.ToListAsync();
                Categories = await _context.Categories.ToListAsync();
                Regions = await _context.Regions.ToListAsync();
                Provinces = await _context.Provinces.ToListAsync();
                return Page();
            }

        }
        public async Task<IActionResult> OnGetGetProductAsync(int id)
        {
            var product = await _context.Foods
                .Where(f => f.Id == id)
                .Select(f => new
                {
                    f.Id,
                    f.Name,
                    f.Slug,
                    f.Description,
                    f.CategoryId,
                    f.RegionId,
                    f.ProvinceId,
                    f.Price,
                    f.SalePrice,
                    f.StockQuantity,
                    f.Unit,
                    f.Weight,
                    f.ShelfLife,
                    f.StorageInstructions,
                    f.IsFeatured,
                    f.IsSpecial,
                    f.IsVegetarian,
                    f.IsActive,
                    f.Ingredients,
                    f.PreparationMethod,
                    f.ServingSuggestion,
                    f.CulturalSignificance,
                    f.Allergens,
                    f.Certification,
                    f.CreatedAt,
                    f.UpdatedAt,
                    images = f.FoodImages.Select(fi => fi.Filename).ToList()
                })
                .FirstOrDefaultAsync();


            if (product == null)
                return NotFound();

            return new JsonResult(product);
        }
        public async Task<IActionResult> OnPostDeleteProductAsync(int id)
        {
            var product = await _context.Foods.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            // Check if product is in any active orders
            var hasActiveOrders = await _context.OrderItems
                .AnyAsync(oi => oi.FoodId == id && oi.Order.OrderStatus != "Cancelled");

            if (hasActiveOrders)
            {
                TempData["Error"] = "Cannot delete product as it is associated with active orders.";
                return RedirectToPage();
            }

            _context.Foods.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditProductAsync(Food product, List<IFormFile> Images)
        {
            try
            {
                var food = await _context.Foods.FindAsync(product.Id);
                if (food == null)
                {
                    return NotFound();
                }

                food.Name = product.Name;
                food.Slug = product.Slug;
                food.Description = product.Description;
                food.CategoryId = product.CategoryId;
                food.RegionId = product.RegionId;
                food.ProvinceId = product.ProvinceId;
                food.Price = product.Price;
                food.SalePrice = product.SalePrice;
                food.StockQuantity = product.StockQuantity;
                food.Unit = product.Unit;
                food.Weight = product.Weight;
                food.ShelfLife = product.ShelfLife;
                food.StorageInstructions = product.StorageInstructions;
                food.IsFeatured = product.IsFeatured;
                food.IsSpecial = product.IsSpecial;
                food.IsVegetarian = product.IsVegetarian;
                food.IsActive = product.IsActive;
                food.Ingredients = product.Ingredients;
                food.PreparationMethod = product.PreparationMethod;
                food.ServingSuggestion = product.ServingSuggestion;
                food.CulturalSignificance = product.CulturalSignificance;
                food.Allergens = product.Allergens;
                food.Certification = product.Certification;

                food.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                if (Images.Count > 0)
                {
                    var oldImages = await _context.FoodImages.Where(fi => fi.FoodId == product.Id).ToListAsync();

                    _context.FoodImages.RemoveRange(oldImages);
                    foreach (var image in Images)
                    {
                        if (image.Length > 0)
                        {
                            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                            var filePath = Path.Combine(_env.WebRootPath, "uploads", fileName);

                            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await image.CopyToAsync(stream);
                            }

                            var foodImage = new FoodImage()
                            {
                                FoodId = product.Id,
                                Filename = "/uploads/" + fileName,
                                CreatedAt = DateTime.UtcNow
                            };

                            _context.FoodImages.Add(foodImage);
                        }
                    }

                    await _context.SaveChangesAsync();
                }

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "L?i khi l?u d? li?u: " + ex.Message);
                Products = await _context.Foods.ToListAsync();
                Categories = await _context.Categories.ToListAsync();
                Regions = await _context.Regions.ToListAsync();
                Provinces = await _context.Provinces.ToListAsync();
                return Page();
            }
        }

    }
}