using ASPNetCore.Entities;
using ASPNetCore.Exceptions;
using ASPNetCore.Utilities;
using System.Xml.Linq;

namespace ASPNetCore.Repositories
{
    public class InMemoryRepository
    {
        private readonly List<Category> _categories = new();
        private readonly List<Item> _items = new();

        private int _categoryIdCounter = 1;
        private int _itemIdCounter = 1;


        public Category CreateCategory(string name)
        {
            var category = new Category
            {
                Id = _categoryIdCounter++,
                Name = name
            };

            _categories.Add(category);
            return category;
        }

        public List<Category> GetAllCategories()
        {
            return _categories.ToList();
        }

        public Category? GetCategoryById(int id)
        {
            return _categories.FirstOrDefault(c => c.Id == id);
        }

        public Category? UpdateCategory(int id, string newName)
        {
            var category = GetCategoryById(id);
            if (category == null) return null;

            category.Name = newName;

            return category;
        }

        public bool DeleteCategory(int id)
        {
            var category = GetCategoryById(id);
            if (category == null) return false;

            _items.RemoveAll(i => i.CategoryId == id);

            return _categories.Remove(category);
        }


        public Item? CreateItem(Item newItem)
        {
            var categoryExists = _categories.Any(c => c.Id == newItem.CategoryId);
            
            if (!categoryExists)
                throw new CategoryNotFoundException(newItem.CategoryId);

            var item = new Item
            {
                Id = _itemIdCounter++,
                Name = newItem.Name,
                CategoryId = newItem.CategoryId
            };

            _items.Add(item);
            return item;
        }

        public List<Item> ListItems(
            int? categoryId = null,
            PaginationParams? pagination = null
        )
        {
            var query = _items.AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(i => i.CategoryId == categoryId.Value);
            }

            if (pagination != null)
            {
                var page = pagination.Page;
                var pageSize = pagination.PageSize;

                query = query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
            }

            return query.ToList();
        }

        public Item? GetItemById(int id)
        {
            return _items.FirstOrDefault(i => i.Id == id);
        }

        public List<Item> GetItemsByCategory(int categoryId)
        {
            return _items
                .Where(i => i.CategoryId == categoryId)
                .ToList();
        }

        public Item? UpdateItem(int id, string newName, int newCategoryId)
        {
            var item = GetItemById(id);

            if (item == null) return null;

            var categoryExists = _categories.Any(c => c.Id == newCategoryId);

            if (!categoryExists)
                throw new CategoryNotFoundException(newCategoryId);

            item.CategoryId = newCategoryId;
            item.Name = newName;

            return item;
        }

        public bool DeleteItem(int id)
        {
            var item = GetItemById(id);
            if (item == null) return false;

            return _items.Remove(item);
        }

        public void ClearAll()
        {
            _categories.Clear();
            _items.Clear();
            _categoryIdCounter = 1;
            _itemIdCounter = 1;
        }
    }
}
