using System;
using System.Collections.Generic;
using System.Linq;
using Keesing.Calendar.Api.Models;
using Keesing.Calendar.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace keesing.Calendar.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarController : ControllerBase
    {
        private readonly ILogger<CalendarController> _logger;
        private readonly ICalendarService _service;

        public CalendarController(ILogger<CalendarController> logger, ICalendarService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post(Event @event)
        {
            if (_service.AddEvent(@event))
                return CreatedAtAction(nameof(Post), @event);

            return BadRequest();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            var isSuccess = _service.DeleteEvent(id);

            if (isSuccess)
                return Ok();
            else
                return NotFound();
        }

        [HttpPut]
        [Route("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Update(int id, Event @event)
        {
            var isSuccess = _service.EditEvent(@event);

            if (isSuccess)
                return Ok(@event);
            else
                return NotFound();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Event>> GetAll()
        {
            var allEvents = _service.GetAllEvents();

            return Ok(allEvents);
        }

        [HttpGet]
        [Route("query")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Event>> GetBy(int? id = null, string name = null, string eventOrganizer = null, string location = null)
        {
            if (id != null)
            {
                return Ok(_service.GetEventById(id.Value));
            }
            else if (name != null)
            {
                return Ok(_service.GetEventByName(name));
            }
            else if (eventOrganizer != null)
            {
                return Ok(_service.GetAllEventsBy(nameof(Event.EventOrganizer), eventOrganizer));
            }
            else if (location != null)
            {
                return Ok(_service.GetAllEventsBy(nameof(Event.Location), location));
            }

            return Ok(_service.GetAllEvents());
        }

        [HttpGet]
        [Route("sort")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Event>> GetAllSorted()
        {
            return Ok(_service.GetAllEventsSortedBy(nameof(Event.Time)));
        }
    }
}
