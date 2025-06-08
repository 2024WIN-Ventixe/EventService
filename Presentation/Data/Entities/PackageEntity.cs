using System.ComponentModel.DataAnnotations;

namespace Presentation.Data.Entities;

public class PackageEntity
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Seating {  get; set; }
    public string? PLacement { get; set; }
    public decimal? Price { get; set; } 

}
