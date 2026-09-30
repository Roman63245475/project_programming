using be;
using Microsoft.AspNetCore.Mvc;
using service;

namespace api;

[Route("api/[controller]")]
public class CategoryController(CategoryService categoryService) : ControllerBase {
    [HttpPost(nameof(CreateCategory))]
    public async Task<ActionResult<ApiResponse>> CreateCategory([FromBody] CategoryDTO categoryDTO) {
        var (isSuccess, message) = await categoryService.CreateCategory(categoryDTO);
        if (!isSuccess) return BadRequest(new ApiResponse(message));
        return Ok(new ApiResponse(message));
    }

    [HttpGet(nameof(get_categories))]
    public async Task<List<Category>> get_categories()
    {
        return await categoryService.get_categories();
    }
}
