using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using Domain;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository) 
        {
            _productRepository = productRepository;
        }

        private ProductDto MapToDto(Product product)
        {
            if (product == null) return null;

            return new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Category =  new CategoryDto
                {
                    CategoryId = product.CategoryId,
                    Name = product.Category.Name,
                } ,
                ProductVariants = product.ProductVariants.Select(pv => new ProductVariantDto
                {
                    ProductVariantId = pv.ProductVariantId,
                    Color = pv.Color,
                    Size = pv.Size,
                    Price = pv.Price,
                    StockQuantity = pv.StockQuantity
                }).ToList() ?? new List<ProductVariantDto>()
            };
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            var product= new Product
            {
                ProductId = Guid.NewGuid().ToString(),
                Name = productDto.Name,
                Description = productDto.Description,
                CategoryId = productDto.CategoryId,
                Category= new Category
                {
                    CategoryId = productDto.CategoryId,
                    Name = productDto.Category.Name,
                },
                ProductVariants = productDto.ProductVariants.Select(pv => new ProductVariant
                {
                    Color = pv.Color,
                    Size = pv.Size,
                    Price = pv.Price,
                    StockQuantity = pv.StockQuantity
                }).ToList()
            };
            foreach(var variant in product.ProductVariants)
            {
                variant.ProductVariantId = Guid.NewGuid().ToString();
                variant.ProductId = product.ProductId;
            }
            await _productRepository.AddAsync(product);
            return MapToDto(product);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductAsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(MapToDto).ToList();
        }

        public async Task<ProductDto> GetProductByIdAsync(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return null;
            }
            return MapToDto(product);
        }

        public async Task UpdateProductAsync(string id, UpdateProductDto product)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                throw new Exception("Product not found");
            }
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.CategoryId = product.CategoryId;

            await _productRepository.UpdateAsync(existingProduct);
        }
    }
}
