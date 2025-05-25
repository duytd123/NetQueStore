using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetQueStore.exe201.Models;

namespace WebApplication1.Pages;

public class IndexModel : PageModel
{
    private readonly Exe2Context _context;

    public List<Food> Foods { get; set; } = new();

    public IndexModel(Exe2Context context)
    {
        _context = context;
    }
               
    public void OnGet()
    {
        Foods = _context.Foods
            .Include(f => f.FoodImages)
            .Include(f => f.Category)
            .Include(f => f.Region)
            .Include(f => f.Province)
            .Where(f => f.IsActive)
            .ToList();
    }
}
