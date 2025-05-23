using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NetQueStore.Pages.Cart;

public class DecreasePartialModel : PageModel
{
    public IActionResult OnGet(int id)
    {
        var cookie = Request.Cookies["shopping_cart"];
        var parts = !string.IsNullOrEmpty(cookie)
            ? cookie.Split('-', StringSplitOptions.RemoveEmptyEntries).ToList()
            : new List<string>();

        var index = parts.IndexOf(id.ToString());
        if (index >= 0)
        {
            parts.RemoveAt(index);
        }

        var newCookie = string.Join("-", parts);

        Response.Cookies.Append("shopping_cart", newCookie, new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddDays(365),
            Path = "/"
        });

        return Partial("_CartDropdownPartial");
    }
}
