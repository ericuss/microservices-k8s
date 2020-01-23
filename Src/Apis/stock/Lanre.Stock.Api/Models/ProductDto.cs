namespace Lanre.Stock.Api.Models
{
    using System;

    public class ProductDto
    {
        public ProductDto()
        {
            this.Id = Guid.NewGuid();
        }
        
        public Guid Id { get; set; }

        public int Stock  { get; set; }
    }
}