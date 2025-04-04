using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Domain;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> CreateProductAsync(ProductDto product);
        Task<ProductDto> GetProductByIdAsync(string id);
        Task<IEnumerable<ProductDto>> GetAllProductAsAsync();
        Task UpdateProductAsync(string id,UpdateProductDto product); 
        Task DeleteProductAsync(string id);
    }
}
