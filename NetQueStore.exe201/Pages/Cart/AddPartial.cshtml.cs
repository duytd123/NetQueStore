using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using NetQueStore.exe201.Models;

namespace NetQueStore.Pages.Cart;

public class AddPartialModel : PageModel
{
    private readonly Exe2Context _context;

    public AddPartialModel(Exe2Context context)
    {
        _context = context;
    }

    public JsonResult OnGet(int id)
    {
        var cookie = Request.Cookies["shopping_cart"];
        var parts = !string.IsNullOrEmpty(cookie)
            ? cookie.Split('-', StringSplitOptions.RemoveEmptyEntries).ToList()
            : new List<string>();

        int currentQuantity = parts.Count(x => x == id.ToString());

        var food = _context.Foods.FirstOrDefault(f => f.Id == id && f.IsActive);
        if (food == null)
        {
            return new JsonResult(new { success = false, message = "Sản phẩm không tồn tại." });
        }

        if (currentQuantity >= food.StockQuantity)
        {
            return new JsonResult(new { success = false, message = "Không thể thêm quá số lượng tồn kho." });
        }

        parts.Add(id.ToString());
        var newCookie = string.Join("-", parts);

        Response.Cookies.Append("shopping_cart", newCookie, new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddDays(365),
            Path = "/"
        });

        string html = RenderPartialViewToString("_CartDropdownPartial");
        return new JsonResult(new { success = true, html });
    }


    private string RenderPartialViewToString(string partialViewName)
    {
        using var writer = new StringWriter();
        var viewResult = (HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine)
            ?.FindView(PageContext, partialViewName, false);

        if (viewResult == null || !viewResult.Success)
        {
            return "<div class='text-danger'>Không thể hiển thị giỏ hàng.</div>";
        }

        var viewContext = new ViewContext(
            PageContext,
            viewResult.View,
            ViewData,
            TempData,
            writer,
            new HtmlHelperOptions()
        );

        viewResult.View.RenderAsync(viewContext).Wait();
        return writer.GetStringBuilder().ToString();
    }
}
