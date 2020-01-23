namespace Lanre.BFFs.Web.Api.Models
{
    using System;

    public class ProductDto
    {
        public ProductDto()
        {
            this.Id = Guid.NewGuid();
        }
        
        public Guid Id { get; set; }

        public string Name  { get; set; }

        public int Stock  { get; set; }
    }
}