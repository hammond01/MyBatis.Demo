using Domain;
using Domain.Entities;
using Domain.Repositories;
namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<int> CreateProductAsync(Product product)
    {
        return await _productRepository.CreateAsync(product);
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        return await _productRepository.UpdateAsync(product);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        return await _productRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
    {
        return await _productRepository.GetByNameAsync(name);
    }

    public async Task CreateProductsInTransactionAsync(IEnumerable<Product> products)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            foreach (var product in products)
            {
                await _productRepository.CreateAsync(product);
            }
            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task<IEnumerable<Product>> FindProductsByCategoriesAsync(List<string> categories)
    {
        return await _productRepository.FindByCategoriesAsync(categories);
    }

    public async Task<IEnumerable<Product>> FindProductsByIdsAsync(List<int> ids)
    {
        return await _productRepository.FindByIdsAsync(ids);
    }

    public async Task<IEnumerable<Product>> SearchProductsByTypeAsync(string? searchType, string? searchValue)
    {
        return await _productRepository.SearchByTypeAsync(searchType, searchValue);
    }

    public async Task<IEnumerable<Product>> ComplexSearchProductsAsync(string? name, List<string>? categories, string? priceRange, bool? inStock, bool? isActive, string? orderBy)
    {
        return await _productRepository.ComplexSearchAsync(name, categories, priceRange, inStock, isActive, orderBy);
    }
}
