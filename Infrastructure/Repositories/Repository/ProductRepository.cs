using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Mappers;
using MyBatis.NET.Core;

namespace Infrastructure.Repositories.Repository;

public class ProductRepository : IProductRepository
{
    private readonly IProductMapper _mapper;

    public ProductRepository(SqlSession session)
    {
        _mapper = session.GetMapper<IProductMapper>();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await Task.Run(() => _mapper.GetById(id));
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await Task.Run(() => _mapper.GetAll());
    }

    public async Task<int> CreateAsync(Product product)
    {
        return await Task.Run(() => _mapper.InsertProduct(product));
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        var result = await Task.Run(() => _mapper.UpdateProduct(product.Id, product.Name, product.Description, product.Price, product.Category, product.Stock, product.IsActive));
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await Task.Run(() => _mapper.DeleteProduct(id));
        return result > 0;
    }

    public async Task<List<Product>> GetByNameAsync(string name)
    {
        return await Task.Run(() => _mapper.SearchProducts(name, null, null, null, null, null));
    }

    public async Task<List<Product>> FindByCategoriesAsync(List<string> categories)
    {
        return await Task.Run(() => _mapper.FindByCategories(categories));
    }

    public async Task<List<Product>> FindByIdsAsync(List<int> ids)
    {
        return await Task.Run(() => _mapper.FindByIds(ids));
    }

    public async Task<List<Product>> SearchByTypeAsync(string? searchType, string? searchValue)
    {
        return await Task.Run(() => _mapper.SearchByType(searchType, searchValue));
    }

    public async Task<List<Product>> ComplexSearchAsync(string? name, List<string>? categories, string? priceRange, bool? inStock, bool? isActive, string? orderBy)
    {
        return await Task.Run(() => _mapper.ComplexSearch(name, categories, priceRange, inStock, isActive, orderBy));
    }
}
