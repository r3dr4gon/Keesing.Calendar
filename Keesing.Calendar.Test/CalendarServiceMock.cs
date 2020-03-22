using Keesing.Calendar.Api.Models;
using Keesing.Calendar.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Keesing.Calendar.Test
{
    class CalendarServiceMock : ICalendarService
    {
        private IDictionary<int, Event> _events = new Dictionary<int, Event>();
        private IDictionary<string, int> _secondaryKey = new Dictionary<string, int>();

        public CalendarServiceMock() { }

        public CalendarServiceMock(Event[] events)
        {
            foreach (var @event in events)
            {
                AddEvent(@event);
            }
        }

        public void AddEvent(Event @event)
        {
            _secondaryKey.Add(@event.Name, @event.Id);
            _events.Add(@event.Id, @event);
        }

        public bool EditEvent(Event @event)
        {
            if (!_events.ContainsKey(@event.Id))
                return false;

            PropertyInfo[] properties = typeof(Event).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(@event) != null)
                    property.SetValue(_events[@event.Id], property.GetValue(@event));
            }

            return true;
        }

        public bool DeleteEvent(int id)
        {
            if (!_events.ContainsKey(id))
                return false;

            _secondaryKey.Remove(_events[id].Name);
            _events.Remove(id);
            return true;
        }

        public Event GetEventById(int id)
        {
            return _events[id];
        }

        public Event GetEventByName(string name)
        {
            return _events[_secondaryKey[name]];
        }

        public Event[] GetAllEvents()
        {
            return _events.Values.ToArray();
        }

        public Event[] GetAllEventsSortedBy(string sortParameter)
        {
            PropertyInfo prop = typeof(Event).GetProperty(sortParameter);

            return _events.Values.OrderByDescending(@event => prop.GetValue(@event, null)).ToArray(); ;
        }
    }
}
