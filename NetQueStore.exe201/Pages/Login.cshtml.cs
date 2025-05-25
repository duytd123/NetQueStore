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

            // N?u ??ng nh?p thành công
            return RedirectToPage("/Index");

            // N?u th?t b?i
            // ModelState.AddModelError(string.Empty, "Tên ??ng nh?p ho?c m?t kh?u không ?úng");
            // return Page();
        }
    }

    public class LoginInput
    {
        [Required(ErrorMessage = "Vui lòng nh?p tên ??ng nh?p")]
        [Display(Name = "Tên ??ng nh?p")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Vui lòng nh?p m?t kh?u")]
        [DataType(DataType.Password)]
        [Display(Name = "M?t kh?u")]
        public string Password { get; set; }

        [Display(Name = "Ghi nh? ??ng nh?p")]
        public bool RememberMe { get; set; }
    }
}
