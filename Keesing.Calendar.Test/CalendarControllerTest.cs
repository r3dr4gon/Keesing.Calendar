using keesing.Calendar.Api.Controllers;
using Keesing.Calendar.Api.Models;
using Keesing.Calendar.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Keesing.Calendar.Test
{
    [TestClass]
    public class CalendarControllerTest
    {
        CalendarController _controller;
        ICalendarService _service;
        ILogger<CalendarController> _logger;

        public CalendarControllerTest()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddEventLog();
            });
            _logger = loggerFactory.CreateLogger<CalendarController>();

            _service = new CalendarServiceMock();
            _controller = new CalendarController(_logger, _service);
        }

        [TestMethod]
        public void GetAll_EmptyDB_WhenCalled_ReturnsOkResult()
        {
            // Act
            var response = _controller.GetAll();

            // Assert
            Assert.AreEqual(typeof(OkObjectResult), response.Result.GetType());
        }

        [TestMethod]
        public void GetAll_EmptyDB_WhenCalled_ReturnsEmptyArray()
        {
            // Act
            var response = _controller.GetAll();
            var result = response.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result.Value);
            var events = result.Value as Event[];
            Assert.AreEqual(events.Length, 0);
        }

        [TestMethod]
        public void Post_GetAll_NotEmptyDB_WhenCalled_ReturnsArray()
        {
            // Arrange
            var fakeEvet = new Event
            {
                Name = "Testing party",
                Time = 0,
                Location = "Here",
                Members = "All by myself",
                EventOrganizer = "Myself"
            };
            _controller.Post(fakeEvet);

            // Act
            var response = _controller.GetAll();
            var result = response.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result.Value);
            var events = result.Value as Event[];
            Assert.AreEqual(events.Length, 1);
        }
    }
}
