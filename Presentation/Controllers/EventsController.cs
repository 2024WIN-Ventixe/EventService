using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Presentation.Services;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController(IEventService eventService) : ControllerBase
{
    private readonly IEventService _eventService = eventService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { 
        var events = await _eventService.GetEventsAsync();
        return Ok(events);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var currentEvent = await _eventService.GetEventAsync(id);
        if (currentEvent.Success && currentEvent.Result != null)
            return Ok(currentEvent.Result);

        return NotFound(currentEvent.Error);   
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEventRequest request)
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await _eventService.CreateEventAsync(request);
        return result.Success ? Ok() : StatusCode(500, result.Error);
    }


}
