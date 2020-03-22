using keesing.Calendar.Api.Controllers;
using Keesing.Calendar.Api.Models;
using Keesing.Calendar.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Keesing.Calendar.Test
{
    [TestClass]
    public class CalendarControllerTestFilledDB
    {
        CalendarController _controller;
        ICalendarService _service;
        ILogger<CalendarController> _logger;

        public CalendarControllerTestFilledDB()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddEventLog();
            });
            _logger = loggerFactory.CreateLogger<CalendarController>();

            var events = new Event[]
            {
                Helpers.FakeEventGen(),
                Helpers.GetBobDrinksEvent(),
                Helpers.GetBobPartyEvent(),
                Helpers.GetJoopOutEvent()
            };

            _service = new CalendarServiceMock(events);
            _controller = new CalendarController(_logger, _service);
        }

        [TestMethod]
        public void Delete_ExistingIdPassed_ReturnsOkResult()
        {
            //Act
            var response = _controller.Delete(Helpers.GetJoopOutEvent().Id);

            // Assert
            Assert.AreEqual(typeof(OkResult), response.GetType());
        }

        [TestMethod]
        public void Edit_ExistingIdPassed_ReturnsOkObjectResult()
        {
            // Arrange
            var joopOutEvent = Helpers.GetJoopOutEvent();
            joopOutEvent.Name = "Let's Update";

            //Act
            var response = _controller.Update(joopOutEvent.Id, joopOutEvent);

            // Assert
            Assert.AreEqual(typeof(OkObjectResult), response.GetType());
        }

        [TestMethod]
        public void Post_GetAll_FilledDB_WhenCalled_ReturnsArray()
        {
            // Act
            var response = _controller.GetAll();
            var result = response.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result.Value);
            var events = result.Value as Event[];
            Assert.AreEqual(events.Length, 4);
        }

        [TestMethod]
        public void GetAllSorted_FilledDB_WhenCalled_ReturnsEmptyArray()
        {
            // Arrange
            int lastEventTime = int.MaxValue;

            // Act
            var response = _controller.GetAllSorted();
            var result = response.Result as OkObjectResult;
            var events = result.Value as Event[];

            // Assert
            Assert.IsNotNull(events);
            foreach (var @event in events)
            {
                Assert.IsTrue(@event.Time <= lastEventTime);
                lastEventTime = @event.Time;
            }
        }
    }
}
