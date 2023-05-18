namespace Company.Models.Dtos;

public class ProductDto
{
        public required uint Price { get; set; }
        public required uint Weight { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        
        public virtual CompanyDto? Company { get; set; }
        public virtual Category Category { get; set; }
}