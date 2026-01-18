using ASPNetCore.DTOs;
using ASPNetCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetCore.Controllers
{
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private CategoriesService _categoriesService;

        public CategoriesController(CategoriesService categoriesService)
        {
            this._categoriesService = categoriesService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreationDTO dto)
        {
            var created = await this._categoriesService.CreateCategoryAsync(dto);

            return CreatedAtAction(nameof(GetCategoryById), new { id = created.Id }, created);
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetCategories()
        {
            var categories = await this._categoriesService.ListCategoriesAsync();

            return categories;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            var category = await this._categoriesService.GetCategoryAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateDTO dto)
        {
            var updatedCategory = await _categoriesService.UpdateCategoryAsync(id, dto);

            if (updatedCategory != null)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deleted = await this._categoriesService.DeleteCategoryAsync(id);

            if (deleted)
            {
                return NoContent();
            } else
            {
                return NotFound();
            }
        }
    }
}
