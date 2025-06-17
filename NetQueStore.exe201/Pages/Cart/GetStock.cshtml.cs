using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetQueStore.exe201.Models;

namespace NetQueStore.exe201.Pages.Cart
{
    public class GetStockModel : PageModel
    {
        private readonly Exe2Context _context;

        public GetStockModel(Exe2Context context)
        {
            _context = context;
        }

        // Nhận query param foodId, ví dụ /Cart/GetStock?foodId=123
        public IActionResult OnGet(int foodId)
        {
            var food = _context.Foods.FirstOrDefault(f => f.Id == foodId && f.IsActive);
            if (food == null)
                return new JsonResult(new { success = false, message = "Không tìm thấy đồ ăn" });

            return new JsonResult(new { success = true, stockQuantity = food.StockQuantity });
        }
    }
}
