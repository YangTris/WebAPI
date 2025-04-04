using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> AddAsync(Category category);
        Task<Category> GetByIdAsync(int id);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
    }
}
