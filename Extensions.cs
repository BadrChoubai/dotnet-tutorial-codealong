using ECommerce.Dtos;
using ECommerce.Entities;

namespace ECommerce
{
    public static class Extensions
    {
        public static ProductDto AsDto(this Product product)
        {
            return new ProductDto
            { Id = product.Id, Name = product.Name, Description = product.Description, Price = product.Price, CreatedDate = product.CreatedDate };
        }
    }
}