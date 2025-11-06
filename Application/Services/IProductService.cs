using Domain.Entities;
namespace Application.Services;

public interface IProductService
{
    Task<Product?> GetProductByIdAsync(int id);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<int> CreateProductAsync(Product product);
    Task<bool> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(int id);
    Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
    Task CreateProductsInTransactionAsync(IEnumerable<Product> products);
    Task<IEnumerable<Product>> FindProductsByCategoriesAsync(List<string> categories);
    Task<IEnumerable<Product>> FindProductsByIdsAsync(List<int> ids);
    Task<IEnumerable<Product>> SearchProductsByTypeAsync(string? searchType, string? searchValue);
    Task<IEnumerable<Product>> ComplexSearchProductsAsync(string? name, List<string>? categories, string? priceRange, bool? inStock, bool? isActive, string? orderBy);
}
