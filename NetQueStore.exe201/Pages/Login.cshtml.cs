using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace NetQueStore.exe201.Pages
{
    public class Login : PageModel
    {
        [BindProperty]
        public LoginInput Input { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Thêm logic xác th?c ??ng nh?p ? ?ây
            // Ví d?: ki?m tra v?i database

            return RedirectToPage("/Index");

            // N?u th?t b?i
            // ModelState.AddModelError(string.Empty, "Tên ??ng nh?p ho?c m?t kh?u không ?úng");
            // return Page();
        }
    }

    public class LoginInput
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Ghi nhớ đăng nhập")]
        public bool RememberMe { get; set; }
    }
}
