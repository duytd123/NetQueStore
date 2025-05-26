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

        public async Task OnGetAsync()
        {
            Orders = await _context.Orders
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync()
        {
            var order = await _context.Orders.FindAsync(SelectedOrderId);
            if (order == null)
            {
                return NotFound();
            }

            if (order.PaymentStatus?.ToLower() == "paid")
            {
                StatusMessage = "Không thể chỉnh sửa đơn hàng đã thanh toán.";
                Orders = await _context.Orders.OrderByDescending(o => o.OrderDate).ToListAsync();
                return Page();
            }

            order.OrderStatus = SelectedOrderStatus;
            await _context.SaveChangesAsync();

            StatusMessage = $"Đã cập nhật trạng thái đơn hàng #{order.OrderNumber} thành '{SelectedOrderStatus}'";

            Orders = await _context.Orders.OrderByDescending(o => o.OrderDate).ToListAsync();

            return Page();
        }

    }
}
