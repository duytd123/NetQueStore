using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetQueStore.exe201.Models;

namespace NetQueStore.exe201.Pages
{
    public class OrderCompleteModel : PageModel
    {
        private readonly Exe2Context _context;

        public OrderCompleteModel(Exe2Context context)
        {
            _context = context;
        }

        public NetQueStore.exe201.Models.Order Order { get; set; }

        public IActionResult OnGet(int id)
        {
            Order = _context.Orders
     .Include(o => o.OrderItems)
     .Include(o => o.PaymentMethod)
     .FirstOrDefault(o => o.Id == id);

            if (Order == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
