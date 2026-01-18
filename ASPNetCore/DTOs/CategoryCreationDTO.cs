using System.ComponentModel.DataAnnotations;

namespace ASPNetCore.DTOs
{
    public class CategoryCreationDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
