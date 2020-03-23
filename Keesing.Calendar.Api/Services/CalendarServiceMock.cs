using Keesing.Calendar.Api.Models;
using Keesing.Calendar.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Keesing.Calendar.Api.Services
{
    public class CalendarServiceMock : ICalendarService
    {
        private readonly IDictionary<int, Event> _events = new Dictionary<int, Event>();
        private readonly IDictionary<string, int> _secondaryKey = new Dictionary<string, int>();

        public CalendarServiceMock() { }

        public CalendarServiceMock(Event[] events)
        {
            foreach (var @event in events)
            {
                AddEvent(@event);
            }
        }

        public bool AddEvent(Event @event)
        {
            if (@event.Name == null || _secondaryKey.ContainsKey(@event.Name))
                return false;

            _secondaryKey.Add(@event.Name, @event.Id);
            _events.Add(@event.Id, @event);
            return true;
        }

        public bool EditEvent(Event @event)
        {
            if (!_events.ContainsKey(@event.Id))
                return false;

            if (@event.Name != null)
            {
                if (_secondaryKey.ContainsKey(@event.Name))
                    if (_secondaryKey[@event.Name] != @event.Id)
                        return false;

                _secondaryKey.Remove(_events[@event.Id].Name);
                _secondaryKey.Add(@event.Name, @event.Id);
            }

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
            if (_events.ContainsKey(id))
                return _events[id];
            else
                return null;
        }

        public Event GetEventByName(string name)
        {
            if (_secondaryKey.ContainsKey(name))
                return _events[_secondaryKey[name]];
            else
                return null;
        }

        public Event[] GetAllEvents()
        {
            return _events.Values.ToArray();
        }

        public Event[] GetAllEventsBy(string filterField, string filterValue)
        {
            PropertyInfo prop = typeof(Event).GetProperty(filterField);

            return _events.Values.Where(@event => prop.GetValue(@event, null).ToString() == filterValue).ToArray();
        }

        public Event[] GetAllEventsSortedBy(string sortParameter)
        {
            PropertyInfo prop = typeof(Event).GetProperty(sortParameter);

            return _events.Values.OrderByDescending(@event => prop.GetValue(@event, null)).ToArray();
        }
    }
}
