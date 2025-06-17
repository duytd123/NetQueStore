using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetQueStore.exe201.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebApplication1.Pages;

public class IndexModel : PageModel
{
    private readonly Exe2Context _context;
    private readonly ILogger<IndexModel> _logger;

    public List<Food> Foods { get; set; } = new();
    public List<Category> Categories { get; set; } = new();

    [BindProperty]
    [Required(ErrorMessage = "Vui lòng nhập email")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    public string Email { get; set; } = null!;

    public string? Message { get; set; }

    public IndexModel(Exe2Context context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public void OnGet()
    {
        _logger.LogInformation("OnGet called at {Time}", DateTime.Now);
        Categories = _context.Categories
            .Take(4)
            .ToList();

        Foods = _context.Foods
            .Include(f => f.FoodImages)
            .Include(f => f.Category)
            .Include(f => f.Region)
            .Include(f => f.Province)
            .Where(f => f.IsActive)
            .OrderByDescending(f => f.StockQuantity)
            .Take(8)
            .ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        _logger.LogInformation("OnPostAsync called with Email: {Email} at {Time}", Email, DateTime.Now);

        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                _logger.LogWarning("ModelState Error: {Error}", error.ErrorMessage);
            }
            Message = "Email không hợp lệ!";
            return Page();
        }

        try
        {
            _logger.LogInformation("Checking for existing email: {Email}", Email);
            var existed = await _context.Messages.AnyAsync(m => m.Email == Email);
            if (existed)
            {
                _logger.LogInformation("Duplicate email found: {Email}", Email);
                Message = "Email này đã được đăng ký trước đó!";
                return Page();
            }

            var newMessage = new Message
            {
                Email = Email,
                Status = "subscribed",
                CreatedAt = DateTime.Now,
                Phone = "",
                Firstname = "unknown",
                Lastname = "unknown",
                Message1 = "Đăng ký nhận email từ trang chủ",
                Subject = "Đăng ký nhận email từ trang chủ",
                IsResolved = false,
                AssignedTo = null,
                UpdatedAt = null
            };

            _logger.LogInformation("Adding new message to database: {Email}", Email);
            _context.Messages.Add(newMessage);
            int rowsAffected = await _context.SaveChangesAsync();
            _logger.LogInformation("Message saved successfully for email: {Email}, Rows affected: {Rows}", Email, rowsAffected);

            Message = "Đăng ký thành công! Cảm ơn bạn đã quan tâm.";
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving message for email: {Email}", Email);
            Message = "Đã xảy ra lỗi, vui lòng thử lại!";
            return Page();
        }
    }
}