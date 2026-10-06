using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopSphere.Application.Abstractions;
using ShopSphere.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Infrastructure
{
    public static class DependencyInjection
    {
       public static IServiceCollection AddInfrastructure(this IServiceCollection services,string connectionString)
        {
            services.AddDbContext<AppDbContext>(o => o.UseSqlServer ( connectionString));
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
