using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetQueStore.exe201.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NetQueStore.exe201.Pages.Admin
{
    public class LoginModel : PageModel
    {
        private readonly Exe2Context _context;

        public LoginModel(Exe2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("AdminLoggedIn") == "true")
            {
                return RedirectToPage("/Admin/Dashboard");
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Vui lòng nhập đủ thông tin.";
                return Page();
            }

            var adminUser = await _context.Users
                .Where(u => u.Email == Email && u.Password == Password && u.Role == "admin" && u.IsActive)
                .FirstOrDefaultAsync();

            if (adminUser == null)
            {
                ErrorMessage = "Email hoặc mật khẩu không đúng, hoặc bạn không có quyền truy cập.";
                return Page();
            }

            HttpContext.Session.SetString("AdminLoggedIn", "true");
            HttpContext.Session.SetString("AdminEmail", adminUser.Email);

            return RedirectToPage("/Admin/Dashboard"); 
        }
    }
}
