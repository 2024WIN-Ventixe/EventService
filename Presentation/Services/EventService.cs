using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Presentation.Data.Entities;
using Presentation.Data.Repositories;
using Presentation.Models;

namespace Presentation.Services;
public class EventService(IEventRespository eventRepository) : IEventService
{
    private readonly IEventRespository _eventRepository = eventRepository;

    public async Task<EventResult> CreateEventAsync(CreateEventRequest request)
    {
        try
        {
            var eventEntity = new EventEntity
            {
                Image = request.Image,
                Name = request.Name,
                Description = request.Description,
                Location = request.Location,
                EventDate = request.EventDate,
                Price = request.Price,
            };
            var result = await _eventRepository.AddAsync(eventEntity);
            return result.Success
                ? new EventResult { Success = true }
                : new EventResult { Success = false, Error = result.Error };
        }
        catch (Exception ex)
        {
            return new EventResult
            {
                Success = false,
                Error = ex.Message,
            };
        }
    }


    public async Task<EventResult<IEnumerable<Event>>> GetEventsAsync()
    {
        var result = await _eventRepository.GetAllAsync();
        var events = result.Result?.Select(x => new Event
        {
            Id = x.Id,
            Image = x.Image,
            Name = x.Name,
            Description = x.Description,
            Location = x.Location,
            EventDate = x.EventDate,
            Price = x.Price,
        });

        return new EventResult<IEnumerable<Event>> { Success = true, Result = events };
    }
    public async Task<EventResult<Event?>> GetEventAsync(string eventid)
    {
        var result = await _eventRepository.GetAsync(x => x.Id == eventid);
        if (result.Success && result.Result != null)
        {
            var currentEvent = new Event
            {
                Id = result.Result.Id,
                Image = result.Result.Image,
                Name = result.Result.Name,
                Description = result.Result.Description,
                Location = result.Result.Location,
                EventDate = result.Result.EventDate,
                Price = result.Result.Price,
            };

            return new EventResult<Event?> { Success = true, Result = currentEvent };
        }

        return new EventResult<Event?> { Success = false, Error = "Event not found" };
    }
}
