namespace ASPNetCore.Exceptions
{
    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException(int categoryId)
            : base($"Category {categoryId} not found") { }
    }
}
