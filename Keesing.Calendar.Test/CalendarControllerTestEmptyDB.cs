using keesing.Calendar.Api.Controllers;
using Keesing.Calendar.Api.Models;
using Keesing.Calendar.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Keesing.Calendar.Test
{
    [TestClass]
    public class CalendarControllerTestEmptyDB
    {
        readonly CalendarController _controller;
        readonly ICalendarService _service;
        readonly ILogger<CalendarController> _logger;

        public CalendarControllerTestEmptyDB()
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
        public void Post_WhenCalled_ReturnsCreatedResult()
        {
            // Arrange
            var fakeEvent = Helpers.FakeEventGen();

            // Act
            var response = _controller.Post(fakeEvent);

            // Assert
            Assert.AreEqual(typeof(CreatedAtActionResult), response.GetType());
        }

        [TestMethod]
        public void Post_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var fakeEvent = Helpers.FakeEventGen();

            // Act
            var response = _controller.Post(fakeEvent);
            var result = response as CreatedAtActionResult;
            var sentEvent = result.Value;

            // Assert
            Assert.AreEqual(typeof(Event), sentEvent.GetType());
            Assert.AreEqual(sentEvent,fakeEvent);
        }

        [TestMethod]
        public void Delete_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            // Act
            var response = _controller.Delete(int.MaxValue); // DB is empty thus any id will work here.

            // Assert
            Assert.AreEqual(typeof(NotFoundResult), response.GetType());
        }

        [TestMethod]
        public void Edit_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var fakeEvent = Helpers.FakeEventGen();
            // Act
            var response = _controller.Update(fakeEvent.Id, fakeEvent); // DB is empty thus any id will work here.

            // Assert
            Assert.AreEqual(typeof(NotFoundResult), response.GetType());
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
        public void GetByEventId_EmptyDB_WhenCalled_ReturnsEvent()
        {
            // Act
            const int _id = int.MaxValue; // DB is empty thus any id will work here.
            var response = _controller.GetBy(id: _id);
            var result = response.Result as OkObjectResult;

            // Assert
            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public void GetByEventName_EmptyedDB_WhenCalled_ReturnEvent()
        {
            // Act
            string _name = "The Joker"; // DB is empty thus any name will work here.
            var response = _controller.GetBy(name: _name);
            var result = response.Result as OkObjectResult;

            // Assert
            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public void GetByEventOrganizer_EmptyDB_WhenCalled_ReturnsArray()
        {
            // Act
            const string _eventOrganizer = "The Joker"; // DB is empty thus any name will work here.
            var response = _controller.GetBy(eventOrganizer: _eventOrganizer);
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
            const string _location = "The Joker House"; // DB is empty thus any location will work here.
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
        public void GetBy_EmptyDB_WhenCalled_ReturnsEmptyArray()
        {
            // Act
            var response = _controller.GetBy();
            var result = response.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result.Value);
            var events = result.Value as Event[];
            Assert.AreEqual(events.Length, 0);
        }

        [TestMethod]
        public void GetAllSorted_EmptyDB_WhenCalled_ReturnsEmptyArray()
        {
            // Act
            var response = _controller.GetAllSorted();
            var result = response.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result.Value);
            var events = result.Value as Event[];
            Assert.AreEqual(events.Length, 0);
        }
    }
}
