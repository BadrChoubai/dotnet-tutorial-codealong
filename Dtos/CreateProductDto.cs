using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos
{
    public record CreateProductDto
    {
        [Required]
        public string Name { get; init; }
        [Required]
        public string Description { get; init; }
        [Required]
        [Range(1, 809)]
        public decimal Price { get; init; }

    }
}