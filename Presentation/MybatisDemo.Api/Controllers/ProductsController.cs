using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MybatisDemo.Api.Controllers;

public record ComplexSearchRequest(
    string? Name,
    List<string>? Categories,
    string? PriceRange,
    bool? InStock,
    bool? IsActive,
    string? OrderBy
);

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpGet("search/{name}")]
    public async Task<IActionResult> GetProductsByName(string name)
    {
        var products = await _productService.GetProductsByNameAsync(name);
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var id = await _productService.CreateProductAsync(product);
        product.Id = id;
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        var result = await _productService.UpdateProductAsync(product);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await _productService.DeleteProductAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("transaction")]
    public async Task<IActionResult> CreateProductsInTransaction([FromBody] IEnumerable<Product> products)
    {
        await _productService.CreateProductsInTransactionAsync(products);
        return Ok();
    }

    [HttpPost("find-by-categories")]
    public async Task<IActionResult> FindProductsByCategories([FromBody] List<string> categories)
    {
        var products = await _productService.FindProductsByCategoriesAsync(categories);
        return Ok(products);
    }

    [HttpPost("find-by-ids")]
    public async Task<IActionResult> FindProductsByIds([FromBody] List<int> ids)
    {
        var products = await _productService.FindProductsByIdsAsync(ids);
        return Ok(products);
    }

    [HttpGet("search-by-type")]
    public async Task<IActionResult> SearchProductsByType([FromQuery] string? searchType, [FromQuery] string? searchValue)
    {
        var products = await _productService.SearchProductsByTypeAsync(searchType, searchValue);
        return Ok(products);
    }

    [HttpPost("complex-search")]
    public async Task<IActionResult> ComplexSearchProducts([FromBody] ComplexSearchRequest request)
    {
        var products = await _productService.ComplexSearchProductsAsync(request.Name, request.Categories, request.PriceRange, request.InStock, request.IsActive, request.OrderBy);
        return Ok(products);
    }
}
