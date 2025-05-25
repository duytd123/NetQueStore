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

            // Th�m logic x�c th?c ??ng nh?p ? ?�y
            // V� d?: ki?m tra v?i database

            // N?u ??ng nh?p th�nh c�ng
            return RedirectToPage("/Index");

            // N?u th?t b?i
            // ModelState.AddModelError(string.Empty, "T�n ??ng nh?p ho?c m?t kh?u kh�ng ?�ng");
            // return Page();
        }
    }

    public class LoginInput
    {
        [Required(ErrorMessage = "Vui l�ng nh?p t�n ??ng nh?p")]
        [Display(Name = "T�n ??ng nh?p")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Vui l�ng nh?p m?t kh?u")]
        [DataType(DataType.Password)]
        [Display(Name = "M?t kh?u")]
        public string Password { get; set; }

        [Display(Name = "Ghi nh? ??ng nh?p")]
        public bool RememberMe { get; set; }
    }
}
