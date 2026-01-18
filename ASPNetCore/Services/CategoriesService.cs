using ASPNetCore.DTOs;
using ASPNetCore.Entities;
using ASPNetCore.Repositories;
using AutoMapper;

namespace ASPNetCore.Services
{
    public class CategoriesService
    {
        private readonly InMemoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoriesService(
            InMemoryRepository repository,
            IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task<List<CategoryDTO>> ListCategoriesAsync()
        {
            var categories = this._repository.GetAllCategories();

            return categories.Select(i => this._mapper.Map<CategoryDTO>(i)).ToList();
        }

        public async Task<CategoryDTO> CreateCategoryAsync(CategoryCreationDTO dto)
        {
            var created = this._repository.CreateCategory(dto.Name);

            return this._mapper.Map<CategoryDTO>(created);
        }

        public async Task<CategoryDTO?> GetCategoryAsync(int id)
        {
            var category = this._repository.GetCategoryById(id);

            return category == null
                ? null
                : this._mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO?> UpdateCategoryAsync(int id, CategoryUpdateDTO dto)
        {
            var updatedCategory = this._repository.UpdateCategory(id, dto.Name);

            return updatedCategory == null
                ? null
                : this._mapper.Map<CategoryDTO>(updatedCategory);
        }
        
        public async Task<Boolean> DeleteCategoryAsync(int id)
        {
            return this._repository.DeleteCategory(id);
        }
    }
}
