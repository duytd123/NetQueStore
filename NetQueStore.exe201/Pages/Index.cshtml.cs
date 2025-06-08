using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetQueStore.exe201.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Pages;

public class IndexModel : PageModel
{
    private readonly Exe2Context _context;

    public List<Food> Foods { get; set; } = new();

    public List<Category> Categories { get; set; } = new();


    [BindProperty]
    [Required(ErrorMessage = "Vui lòng nhập email")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    public string Email { get; set; } = null!;

    public string? Message { get; set; }

    public IndexModel(Exe2Context context)
    {
        _context = context;
    }
               


    public void OnGet()
    {
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
        if (!ModelState.IsValid)
        {
            OnGet();
            return Page();
        }

        var existed = await _context.Messages.AnyAsync(m => m.Email == Email);
        if (existed)
        {
            Message = "Email này đã được đăng ký trước đó!";
            OnGet();
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
            IsResolved = false
        };

        _context.Messages.Add(newMessage);
        await _context.SaveChangesAsync();

        Message = "Đăng ký thành công! Cảm ơn bạn đã quan tâm.";
        OnGet();
        return Page();
    }
}

