using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NetQueStore.Pages.Cart;

public class AddPartialModel : PageModel
{
    public IActionResult OnGet(int id)
    {
        var cookie = Request.Cookies["shopping_cart"];
        var parts = !string.IsNullOrEmpty(cookie)
            ? cookie.Split('-', StringSplitOptions.RemoveEmptyEntries).ToList()
            : new List<string>();

        parts.Add(id.ToString());

        var newCookie = string.Join("-", parts);

        Response.Cookies.Append("shopping_cart", newCookie, new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddDays(365),
            Path = "/"
        });

        return Partial("_CartDropdownPartial");
    }
}
