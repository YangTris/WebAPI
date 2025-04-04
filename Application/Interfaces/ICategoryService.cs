using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Application.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> GetCategoryByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllCategory();
        Task<Category> CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
    }
}
