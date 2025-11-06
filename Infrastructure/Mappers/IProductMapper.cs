using Domain.Entities;

namespace Infrastructure.Mappers;

/// <summary>
/// Auto-generated from ProductMapper.xml
/// Generated at: 2025-11-06 10:13:39
/// DO NOT EDIT - Regenerate from XML using: mybatis-gen generate ProductMapper.xml
/// </summary>
public interface IProductMapper
{
    List<Product> GetAll();

    Product? GetById(int id);

    List<Product> SearchProducts(string? name, string? category, decimal? minPrice, decimal? maxPrice, bool? isActive, int? minStock);

    List<Product> FindByCategories(List<string> categories);

    List<Product> FindByIds(List<int> ids);

    List<Product> SearchByType(string? searchType, string? searchValue);

    List<Product> ComplexSearch(string? name, List<string>? categories, string? priceRange, bool? inStock, bool? isActive, string? orderBy);

    int InsertProduct(Product product);

    int UpdateProduct(int Id, string? Name, string? Description, decimal? Price, string? Category, int? Stock, bool? IsActive);

    int DeleteProduct(int id);

    int CountProducts(string? category, bool? isActive);
}
