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
        readonly CalendarController _controller;
        readonly ICalendarService _service;
        readonly ILogger<CalendarController> _logger;

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
        public void Post_NameNotUnique_WhenCalled_ReturnsBadRequestResult()
        {
            // Arrange
            var bobDrinksEvent = Helpers.GetBobDrinksEvent();

            // Act
            var response = _controller.Post(bobDrinksEvent);

            // Assert
            Assert.AreEqual(typeof(BadRequestResult), response.GetType());
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
        public void GetAll_FilledDB_WhenCalled_ReturnsArray()
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
        public void GetByEventId_FilledDB_WhenCalled_ReturnsEvent()
        {
            // Act
            const int _id = 0;
            var response = _controller.GetBy(id: _id);
            var result = response.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result.Value);
            var @event = result.Value as Event;
            Assert.AreEqual(@event.Id, _id);
        }

        [TestMethod]
        public void GetByEventName_FilledDB_WhenCalled_ReturnEvent()
        {
            // Act
            string _name = Helpers.GetJoopOutEvent().Name;
            var response = _controller.GetBy(name: _name);
            var result = response.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result.Value);
            var @event = result.Value as Event;
            Assert.AreEqual(@event.Name, _name);
        }

        [TestMethod]
        public void GetByEventOrganizer_FilledDB_WhenCalled_ReturnsArray()
        {
            // Act
            const string _eventOrganizer = "Bob";
            var response = _controller.GetBy(eventOrganizer : _eventOrganizer);
            var result = response.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result.Value);
            var events = result.Value as Event[];
            foreach (var @event in events)
            {
                Assert.IsTrue(@event.EventOrganizer == _eventOrganizer);
            }
        }

        [TestMethod]
        public void GetByEventLocation_FilledDB_WhenCalled_ReturnsArray()
        {
            // Act
            const string _location = "Bob House";
            var response = _controller.GetBy(location: _location);
            var result = response.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result.Value);
            var events = result.Value as Event[];
            foreach (var @event in events)
            {
                Assert.IsTrue(@event.Location == _location);
            }
        }

        [TestMethod]
        public void GetBy_FilledDB_WhenCalled_ReturnsNotFilteredDB()
        {
            // Act
            var response = _controller.GetBy();
            var result = response.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result.Value);
            var events = result.Value as Event[];
            Assert.IsTrue(events.Length == 4);
        }

        [TestMethod]
        public void GetAllSorted_FilledDB_WhenCalled_ReturnsSortedArray()
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
