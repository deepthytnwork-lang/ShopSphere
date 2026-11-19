using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.Application.Products;
using ShopSphere.Domain;

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
        [HttpGet]
        public IActionResult Create() => View(new ProductRequest("", null, 0, 0));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>create(ProductRequest pr)
        {
            try
            {
                await _svc.CreateAsync(pr);
                TempData["Success"]= "Product created.";
                return RedirectToAction(nameof(Index));
            }
            catch(DomainException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(pr); ;
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var p = await _svc.GetByIDAsync(id);
            if (p is null) return NotFound();
            ViewBag.Id = id;
            return View(new ProductRequest(p.Name, p.Description, p.Price, p.Stock));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,ProductRequest pr)
        {
            try
            {
                if (!await _svc.UpdateAsync(id, pr)) return NotFound();
                TempData["Success"] = "Product Updated";
                return RedirectToAction(nameof(Index));
            }catch(DomainException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.Id = id;
                return View(pr);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _svc.DeleteAsync(id);
            TempData["Success"] = "Product deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}
