using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetQueStore.exe201.Models;
using Microsoft.EntityFrameworkCore;

namespace NetQueStore.exe201.Pages.Order
{
    public class IndexModel : PageModel
    {
        private readonly Exe2Context _context;
        private readonly int pageSize = 5;

        public List<Models.Order> listOrders = new();
        public int page = 1;
        public int totalPages = 0;

        [BindProperty(SupportsGet = true)]
        public string? OrderNumber { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SessionId { get; set; }

        public IndexModel(Exe2Context context)
        {
            _context = context;
        }

        public void OnGet()
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.Query["page"]))
                {
                    page = int.Parse(Request.Query["page"]);
                }
            }
            catch
            {
                page = 1;
            }

            IQueryable<Models.Order> query = _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.PaymentMethod)
                .AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                // Tìm theo mã đơn hàng hoặc số điện thoại (exact match)
                query = query.Where(o => o.OrderNumber == SearchTerm || o.RecipientPhone == SearchTerm);
            }
            else if (!string.IsNullOrEmpty(HttpContext.Session.GetString("session_id")))
            {
                string sessionId = HttpContext.Session.GetString("session_id")!;
                query = query.Where(o => o.SessionId == sessionId);
            }
            else
            {
                listOrders = new();
                return;
            }

            int totalOrders = query.Count();
            totalPages = (int)Math.Ceiling(totalOrders / (double)pageSize);

            listOrders = query
                .OrderByDescending(o => o.OrderDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

    }
}