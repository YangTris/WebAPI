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
        Task<Category> GetCategoryAsAsync(int id);
        Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}
