using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NetQueStore.exe201.Pages.Cart
{
    public class DeletePartialModel : PageModel
    {
        public IActionResult OnGet(int id)
        {
            var cookie = Request.Cookies["shopping_cart"];
            if (!string.IsNullOrEmpty(cookie))
            {
                var parts = cookie.Split('-', StringSplitOptions.RemoveEmptyEntries).ToList();
                parts.Remove(id.ToString());
                var newCookie = string.Join("-", parts);
                Response.Cookies.Append("shopping_cart", newCookie, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(365),
                    Path = "/"
                });
            }

            return Partial("_CartDropdownPartial");
        }
    }
}
