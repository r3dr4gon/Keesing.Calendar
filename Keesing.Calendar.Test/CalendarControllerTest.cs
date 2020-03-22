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
        public void Delete_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var fakeEvent = Helpers.FakeEventGen();
            _controller.Post(fakeEvent);

            //Act
            var response = _controller.Delete(fakeEvent.Id);

            // Assert
            Assert.AreEqual(typeof(OkObjectResult), response.GetType());
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
            var fakeEvent = Helpers.FakeEventGen();
            _controller.Post(fakeEvent);

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
