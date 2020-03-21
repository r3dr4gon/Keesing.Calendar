using System;
using keesing.Calendar.Api.Models;
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

        public CalendarController(ILogger<CalendarController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post(Event @event)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, Event @event)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("query")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetBy(int? id, string name, string eventOrganizer, string location)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("sort")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllSorted()
        {
            throw new NotImplementedException();
        }
    }
}
