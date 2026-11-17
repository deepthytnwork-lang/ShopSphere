using Microsoft.AspNetCore.Mvc;
using ShopSphere.Application.Products;

namespace ShopSphere.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _svc;

        public ProductsController(ProductService svc) => _svc = svc;



        public async Task<IActionResult> Index()
        {
            var products = await _svc.GetAllAsync();
            return View(products);
        }
    }
}
