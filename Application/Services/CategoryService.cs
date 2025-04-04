using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using Domain;
using Domain.IRepositories;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        private static CategoryDto MaptoDTO(Category category)
        {
            if (category == null)
            {
                return null;
            }

            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
            };
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
            };
           
            await _categoryRepository.AddAsync(category);
            return MaptoDTO(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategory()
        {
            var category = await _categoryRepository.GetAllAsync();
            return category.Select(MaptoDTO).ToList();
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return null;
            }
            return MaptoDTO(category);
        }

        public async Task UpdateCategoryAsync(int id, CategoryDto category)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id);
            if (existingCategory == null)
            {
                throw new Exception("Category not found");
            }
            existingCategory.Name = category.Name;
            await _categoryRepository.UpdateAsync(existingCategory);
        }
    }
}
