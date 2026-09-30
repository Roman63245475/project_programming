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
}
