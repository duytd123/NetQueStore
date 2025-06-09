using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetQueStore.exe201.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace NetQueStore.exe201.Pages
{
    public class AboutmeModel : PageModel
    {
        private readonly Exe2Context _context;

        public AboutmeModel(Exe2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Message NewMessage { get; set; }

        public string? ErrorMessage { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = null!;
        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove(nameof(NewMessage.Status)); // Bỏ validate Status

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                ErrorMessage = "Vui lòng nhập đúng thông tin: " + string.Join("; ", errors);
                return Page();
            }
            if (!Regex.IsMatch(NewMessage.Phone, @"^(0|\+84)(3|5|7|8|9)\d{8}$"))
            {
                ModelState.AddModelError("NewMessage.Phone", "Số điện thoại không đúng định dạng Việt Nam");
            }

            // Kiểm tra số lượng email đã gửi hôm nay
            var today = DateTime.Today;
            int count = await _context.Messages
                .Where(m => m.Email == NewMessage.Email
                        && m.CreatedAt >= today
                        && m.CreatedAt < today.AddDays(1))
                .CountAsync();

            if (count >= 3)
            {
                ErrorMessage = "Bạn đã gửi quá 3 tin nhắn trong ngày hôm nay. Vui lòng thử lại ngày mai.";
                return Page();
            }

            // Gán các trường hệ thống
            NewMessage.Status = "unread";
            NewMessage.IsResolved = false;
            NewMessage.CreatedAt = DateTime.Now;

            try
            {
                _context.Messages.Add(NewMessage);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Gửi tin nhắn thành công!";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ErrorMessage = "Đã xảy ra lỗi: " + ex.Message;
                return Page();
            }
        }
    }
}

