using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Domain;

namespace Application.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto> CreateCategoryAsync(CategoryDto category);
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task<IEnumerable<CategoryDto>> GetAllCategory();
        Task UpdateCategoryAsync(int id,CategoryDto category);
        Task DeleteCategoryAsync(int id);
    }
}
