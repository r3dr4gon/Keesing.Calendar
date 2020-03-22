using Keesing.Calendar.Api.Models;
using System;

namespace Keesing.Calendar.Api.Services
{
    public class CalendarService : ICalendarService
    {
        public void AddEvent(Event @event)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEvent(int id)
        {
            throw new NotImplementedException();
        }

        public bool EditEvent(Event @event)
        {
            throw new NotImplementedException();
        }

        public Event GetEventById(int id)
        {
            throw new NotImplementedException();
        }

        public Event GetEventByName(string name)
        {
            throw new NotImplementedException();
        }

        public Event[] GetAllEvents()
        {
            throw new NotImplementedException();
        }

        public Event[] GetAllEventsBy(string filterField, string filterValue)
        {
            throw new NotImplementedException();
        }

        public Event[] GetAllEventsSortedBy(string sortParameter)
        {
            throw new NotImplementedException();
        }
    }
}
