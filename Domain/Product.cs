using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Product
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Colour { get; set; }
    public required decimal Price { get; set; }
}
