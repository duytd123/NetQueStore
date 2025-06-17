using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetQueStore.exe201.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace NetQueStore.exe201.Pages;

public class SearchModel : PageModel
{

    private readonly Exe2Context _context;
    private readonly ILogger<SearchModel> _logger;

    public SearchModel(Exe2Context context, ILogger<SearchModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    [BindProperty(SupportsGet = true)]
    public string Query { get; set; }

    public List<Food> Foods { get; set; } = new();

    public string Message { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        _logger.LogInformation("OnGetAsync called with Query: '{Query}' at {Time}", Query ?? "null", DateTime.Now);

        if (string.IsNullOrWhiteSpace(Query))
        {
            _logger.LogWarning("Query is empty or null");
            Message = "Vui lòng nhập từ khóa tìm kiếm.";
            return Page();
        }

        Query = Query.Trim();
        if (Query.Length < 3)
        {
            _logger.LogWarning("Query too short: '{Query}'", Query);
            Message = "Từ khóa tìm kiếm phải có ít nhất 3 ký tự.";
            return Page();
        }

        try
        {
            string normalizedQuery = NormalizeVietnamese(Query).ToLower();
            _logger.LogInformation("Normalized Query: '{NormalizedQuery}'", normalizedQuery);

            // Tải dữ liệu trước rồi lọc bằng C#
            var allFoods = await _context.Foods
                .Include(f => f.FoodImages)
                .Include(f => f.Category)
                .Include(f => f.Province)
                .Where(f => f.IsActive)
                .ToListAsync();

            Foods = allFoods
                .Where(f => NormalizeVietnamese(f.Name).ToLower().Contains(normalizedQuery))
                .OrderBy(f => f.Name)
                .Take(20)
                .ToList();

            _logger.LogInformation("Found {Count} products for Query: '{Query}'", Foods.Count, Query);

            if (Foods.Count == 0)
            {
                Message = $"Không tìm thấy sản phẩm phù hợp với từ khóa \"{Query}\".";
            }

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching for Query: '{Query}'", Query);
            Message = "Đã xảy ra lỗi khi tìm kiếm, vui lòng thử lại.";
            return Page();
        }
    }

    public async Task<IActionResult> OnGetSuggestionsAsync()
    {
        _logger.LogInformation("OnGetSuggestionsAsync called with Query: '{Query}' at {Time}", Query ?? "null", DateTime.Now);

        if (string.IsNullOrWhiteSpace(Query) || Query.Length < 3)
        {
            _logger.LogWarning("Invalid query for suggestions: '{Query}'", Query ?? "null");
            return new JsonResult(new List<object>());
        }

        try
        {
            Query = Query.Trim();
            string normalizedQuery = NormalizeVietnamese(Query);
            _logger.LogInformation("Normalized Query for suggestions: '{NormalizedQuery}'", normalizedQuery);

            var results = await _context.Foods
                .Where(f => f.IsActive && EF.Functions.Like(f.Name.ToLower(), $"%{Query.ToLower()}%"))
                .Select(f => new
                {
                    id = f.Id,
                    name = f.Name,
                    image = f.FoodImages.FirstOrDefault(i => i.IsPrimary).Filename ?? "default.png"
                })
                .Take(5)
                .ToListAsync();

            _logger.LogInformation("Found {Count} suggestions for Query: '{Query}'", results.Count, Query);
            return new JsonResult(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching suggestions for Query: '{Query}'", Query);
            return new JsonResult(new List<object>());
        }
    }

    // Helper method to normalize Vietnamese diacritics
    private string NormalizeVietnamese(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;

        // Replace Vietnamese diacritics with base Latin characters
        input = input.Normalize(System.Text.NormalizationForm.FormD);
        var regex = new Regex("\\p{M}");
        return regex.Replace(input, "").Normalize(System.Text.NormalizationForm.FormC);
    }
}