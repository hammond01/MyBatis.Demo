using Domain.Entities;
namespace Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);
    Task<List<Product>> GetAllAsync();
    Task<int> CreateAsync(Product product);
    Task<bool> UpdateAsync(Product product);
    Task<bool> DeleteAsync(int id);
    Task<List<Product>> GetByNameAsync(string name);
    Task<List<Product>> FindByCategoriesAsync(List<string> categories);
    Task<List<Product>> FindByIdsAsync(List<int> ids);
    Task<List<Product>> SearchByTypeAsync(string? searchType, string? searchValue);
    Task<List<Product>> ComplexSearchAsync(string? name, List<string>? categories, string? priceRange, bool? inStock, bool? isActive, string? orderBy);
}
