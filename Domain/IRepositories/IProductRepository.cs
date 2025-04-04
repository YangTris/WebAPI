using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> AddAsync(Product product);
        Task<Product> GetByIdAsync(string id);
        Task UpdateAsync(Product product);
        Task DeleteAsync(string id);
    }
}
