using ShopSphere.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopSphere.Application.Abstractions;
using ShopSphere.Domain;

namespace ShopSphere.Application.Products
{
    public record ProductDto(int Id,string Name,string? Description, decimal Price, int Stock);
    public record ProductRequest(string Name,String?  Description,decimal Price,int Stock);
    public class ProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IProductRepository repo)=>_repo = repo;

        public async Task<List<ProductDto>> GetAllAsync(CancellationToken ct = default) =>
      (await _repo.GetAllAsync(ct)).Select(ToDto).ToList();
        public async Task<ProductDto?> GetByIDAsync(int id,CancellationToken ct=default)
        {
            var p = await _repo.GetbyIdAsync(id, ct);
            return p is null ? null : ToDto(p);
        }
        public async Task<ProductDto> CreateAsync(ProductRequest r,CancellationToken ct=default)
        {
            var product=new Product(r.Name,r.Price,r.Stock,r.Description);
            await _repo.AddAsync(product);
            await _repo.SaveChangesAsync(ct);
            return ToDto(product);
        }
        public async Task<bool> UpdateAsync(int id,ProductRequest r,CancellationToken ct)
        {
            var product = await _repo.GetbyIdAsync(id, ct);
            if(product is null) return false;
            product.update(r.Name, r.Price,r.Stock,r.Description);
            await _repo.SaveChangesAsync(ct);
            return true;
        }
        public async Task<bool> DeleteAsync(int id,CancellationToken ct=default)
        {
            var product=await _repo.GetbyIdAsync(id,ct);
            if(product is null) return false;
            _repo.Remove(product);
            return true;
        }
        private static ProductDto ToDto(Product p) => new(p.Id, p.Name, p.Description, p.Price, p.Stock);
    }
}
