using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetQueStore.exe201.Models;

namespace NetQueStore.exe201.Pages.Admin
{
    public class ManageOrder : PageModel
    {
        public List<Order> Orders { get; set; }

        public void OnGet()
        {
        }
    }
}
