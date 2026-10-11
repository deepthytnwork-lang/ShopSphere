using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopSphere.Domain;
namespace ShopSphere.Application.Abstractions
{
    public  interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(CancellationToken ct = default);
        Task<Product?> GetbyIdAsync(int id,CancellationToken ct=default);
        Task AddAsync(Product product, CancellationToken ct = default);
        Task SaveChangesAsync(CancellationToken ct = default);
        void Remove(Product product);
    }
}
