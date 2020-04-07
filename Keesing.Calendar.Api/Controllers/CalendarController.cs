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

        /// <summary>
        /// Add a new event to the Calendar.
        /// </summary>
        /// <remarks>Name field is required.</remarks>
        /// <response code="201">Event created.</response>
        /// <response code="400">Event could not be added due to a name field not being unique.</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post(Event @event)
        {
            var isSuccess = _service.AddEvent(@event);

            if (isSuccess)
                return CreatedAtAction(nameof(Post), @event);
            else
                return BadRequest();
        }

        /// <summary>
        /// Delete an event with id = {id} from the Calendar.
        /// </summary>
        /// <remarks></remarks>
        /// <response code="200">Event deleted.</response>
        /// <response code="404">Event could not be found.</response>
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

        /// <summary>
        /// Edit an event with id = {id} in the Calendar.
        /// </summary>
        /// <remarks>The Event obj (sent in the request body) must have the same id.</remarks>
        /// <response code="200">Event was updated successfully.</response>
        /// <response code="404">Event could not be found or updated name is not unique.</response>
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

        /// <summary>
        /// Get all events from the Calendar.
        /// </summary>
        /// <remarks></remarks>
        /// <response code="200">All events were fetched.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Event>> GetAll()
        {
            var allEvents = _service.GetAllEvents();

            return Ok(allEvents);
        }

        /// <summary>
        /// Get all events with matching filter condition from the Calendar.
        /// </summary>
        /// <remarks>Events will be fetch according to only one filter parameter.</remarks>
        /// <response code="200">All events were fetched.</response>
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

        /// <summary>
        /// Get all events sorted by time (desc order) from the Calendar.
        /// </summary>
        /// <remarks></remarks>
        /// <response code="200">All events were fetched and are ordered by time.</response>
        [HttpGet]
        [Route("sort")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Event>> GetAllSorted()
        {
            return Ok(_service.GetAllEventsSortedBy(nameof(Event.Time)));
        }
    }
}
