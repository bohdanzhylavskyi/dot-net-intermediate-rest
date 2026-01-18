using System.ComponentModel.DataAnnotations;

namespace ASPNetCore.DTOs
{
    public class ItemCreationDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
