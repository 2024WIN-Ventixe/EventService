using System.ComponentModel.DataAnnotations;

namespace Presentation.Data.Entities;

public class EventEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Image { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Location { get; set; }
    public DateTime? EventDate { get; set; }
    public decimal? Price { get; set; }

    public ICollection<EventPackageEntity> Packages { get; set; } = [];
}
