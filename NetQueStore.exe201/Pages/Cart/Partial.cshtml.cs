using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NetQueStore.exe201.Pages.Cart
{
    public class PartialModel : PageModel
    {
        public IActionResult OnGet() => Partial("_CartDropdownPartial");
    }
}
