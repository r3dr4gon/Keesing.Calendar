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
            var result = _controller.GetAll();

            // Assert
            Assert.AreEqual(typeof(OkObjectResult), result.Result);
        }
    }
}
