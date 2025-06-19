using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetQueStore.exe201.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetQueStore.exe201.Pages.Admin
{
    public class ManageOrderModel : PageModel
    {
        private readonly Exe2Context _context;

        public ManageOrderModel(Exe2Context context)
        {
            _context = context;
        }

        public List<NetQueStore.exe201.Models.Order> Orders { get; set; } = new();

        [BindProperty]
        public int SelectedOrderId { get; set; }

        [BindProperty]
        public string SelectedOrderStatus { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public const int PageSize = 10;

        public async Task<IActionResult> OnGetAsync([FromQuery(Name = "page")] int page = 1)
        {
            Console.WriteLine($"Received page parameter: {page}");
            var isLoggedIn = HttpContext.Session.GetString("AdminLoggedIn");
            if (string.IsNullOrEmpty(isLoggedIn))
            {
                return RedirectToPage("/Admin/Login");
            }

            CurrentPage = page < 1 ? 1 : page;
            var totalOrders = await _context.Orders.CountAsync();
            TotalPages = (int)Math.Ceiling(totalOrders / (double)PageSize);

            Console.WriteLine($"OnGetAsync - Page: {CurrentPage}, Total Orders: {totalOrders}, Total Pages: {TotalPages}");

            Orders = await _context.Orders
                .OrderByDescending(o => o.Id)
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            Console.WriteLine($"Orders Count on Page {CurrentPage}: {Orders.Count}");
            foreach (var order in Orders)
            {
                Console.WriteLine($"Order ID: {order.Id}, Order Number: {order.OrderNumber}, Date: {order.OrderDate}");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync(int SelectedOrderId, string SelectedOrderStatus, int page)
        {
            var order = await _context.Orders.FindAsync(SelectedOrderId);
            if (order == null)
            {
                return NotFound();
            }

            if (order.PaymentStatus?.ToLower() == "paid")
            {
                StatusMessage = "Không thể chỉnh sửa đơn hàng đã thanh toán.";
            }
            else
            {
                order.OrderStatus = SelectedOrderStatus;
                await _context.SaveChangesAsync();
                StatusMessage = $"Đã cập nhật trạng thái đơn hàng #{order.OrderNumber} thành '{SelectedOrderStatus}'";
            }

            return RedirectToPage(new { page });
        }

    }
}
