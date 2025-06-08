namespace Presentation.Models;

public class Event
{
    public string Id { get; set; } = null!;
    public string? Image { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Location { get; set; }
    public DateTime? EventDate { get; set; }
    public decimal? Price { get; set; }
}
