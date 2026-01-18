using ASPNetCore.DTOs;
using ASPNetCore.Entities;
using ASPNetCore.Repositories;
using ASPNetCore.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ASPNetCore.Services
{
    public class ItemsService
    {
        private readonly InMemoryRepository _repository;
        private readonly IMapper _mapper;

        public ItemsService(InMemoryRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }


        public async Task<List<ItemDTO>> ListItemsAsync(
            int? categoryId = null,
            PaginationParams? pagination = null)
        {
            var items = this._repository.ListItems(categoryId, pagination);

            return items.Select(i => this._mapper.Map<ItemDTO>(i)).ToList();

        }

        public async Task<ItemDTO> CreateItemAsync(ItemCreationDTO dto)
        {
            var item = this._mapper.Map<Item>(dto);

            var created = this._repository.CreateItem(item);

            return this._mapper.Map<ItemDTO>(created);
        }

        public async Task<ItemDTO?> GetItemAsync(int id)
        {
            var item = this._repository.GetItemById(id);

            return item == null
                ? null
                : this._mapper.Map<ItemDTO>(item);
        }

        public async Task<ItemDTO?> UpdateItemAsync(int id, ItemUpdateDTO dto)
        {
            var updatedItem = this._repository.UpdateItem(id, dto.Name, dto.CategoryId);

            return updatedItem == null
                ? null
                : this._mapper.Map<ItemDTO>(updatedItem);
        }

        public async Task<Boolean> DeleteItemAsync(int id)
        {
            return this._repository.DeleteItem(id);
        }
    }
}
