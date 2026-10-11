using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Domain
{
    public  class Product
    {
        public int Id { get;private set; }
        public string Name { get; private set; } = default!;
        public string? Description { get; private set; }
        public decimal Price {  get; private set; }
        public int Stock {  get; private set; }
        private Product() { }
        public Product(string name, decimal price, int stock,String? description=null)
        {
            Validate(name, price, stock, description);
            Name = name;Price = price;Stock=stock;Description = description;
                    
                    }
        public void update(string name, decimal price, int stock, String? description = null)
        {
            Validate(name, price, stock, description);
            Name = name;Price = price;Stock=stock;Description = description;
        }
           private static void Validate(string name, decimal price, int stock, String? description = null)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");
            if (price <= 0) throw new ArgumentException("Price must be positive");
            if (stock <= 0) throw new ArgumentException("Stock cannot be negetive ");
        }
       

        }
}
