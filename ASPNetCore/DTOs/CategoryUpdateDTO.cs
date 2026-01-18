using System.ComponentModel.DataAnnotations;

namespace ASPNetCore.DTOs
{
    public class CategoryUpdateDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
