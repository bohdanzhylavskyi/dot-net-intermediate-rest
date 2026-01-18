using ASPNetCore.DTOs;
using ASPNetCore.Exceptions;
using ASPNetCore.Services;
using ASPNetCore.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetCore.Controllers
{
    
    [Route("api/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private ItemsService _itemsService;

        public ItemsController(ItemsService itemsService)
        {
            this._itemsService = itemsService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] ItemCreationDTO dto)
        {
            try
            {
                var created = await this._itemsService.CreateItemAsync(dto);

                return CreatedAtAction(nameof(GetItemById), new { id = created.Id }, created);
            } catch (CategoryNotFoundException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<ItemDTO>>> GetItems(
            [FromQuery] int? categoryId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Query parameters 'page' and 'pageSize' must be positive integers.");
            }

            var paginationParams = new PaginationParams(page, pageSize);
            var items = await this._itemsService.ListItemsAsync(categoryId, paginationParams);

            return items;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItemById(int id)
        {
            var item = await this._itemsService.GetItemAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] ItemUpdateDTO dto)
        {
            try
            {
                var updated = await _itemsService.UpdateItemAsync(id, dto);

                if (updated == null)
                {
                    return NotFound();
                }

                return Ok();
            } catch (CategoryNotFoundException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var deleted = await this._itemsService.DeleteItemAsync(id);

            if (deleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

    }
}
