using Presentation.Models;

namespace Presentation.Services
{
    public interface IEventService
    {
        Task<EventResult> CreateEventAsync(CreateEventRequest request);
        Task<EventResult<Event?>> GetEventAsync(string eventid);
        Task<EventResult<IEnumerable<Event>>> GetEventsAsync();
    }
}