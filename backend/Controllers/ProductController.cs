using backend.Dtos;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
    /* GET: api/v1.product */
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await productService.GetAllAsync();
        return Ok(products);
    }
    
    /* GET: api/v1/product/{id} */
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await productService.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound(new { Message = $"Product with Id = {id} not found." });
        }

        return Ok(product);
    }
    
    /* POST : api/v1/product */
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdProduct = await productService.CreateAsync(dto);
        return Ok(createdProduct);
    }
    
    /* PUT: api/v1/product/{id} */
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateProductDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest(new { Message = "Id in URL and Payload must match." });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updated = await productService.UpdateAsync(id, dto);
        if (!updated)
        {
            return NotFound(new { Message = $"Product with Id = {id} not found." });
        }

        return NoContent();
    }
    
    
    /* DELETE : api/v1/product/{id} */
    [AllowAnonymous]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await productService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new { Message = $"Product with Id = {id} not found." });
        }

        return NoContent();
    }
}