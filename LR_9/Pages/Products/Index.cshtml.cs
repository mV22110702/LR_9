using Microsoft.AspNetCore.Mvc;
using LR_9.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LR_9.Pages.Products
{
    public class IndexModel : PageModel
    {

        public List<Product> Products { get; set; } = new List<Product>() {
                new Product { Id = 0, Name = "Pineapple", Price = 75.28M, },
                new Product { Id = 1, Name = "Watermelon", Price = 100.56M, },
                new Product { Id = 2, Name = "Banana", Price = 15.70M, },
                new Product { Id = 3, Name = "Orange", Price = 25.50M, },
            };

        public PageResult OnGet()
        {
            return Page();
        }
    }
}
