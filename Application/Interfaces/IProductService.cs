using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(Product product);
        Task<Product> GetProductByIdAsync(string id);
        Task<IEnumerable<Product>> GetAllProduct();
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(string id);
    }
}
