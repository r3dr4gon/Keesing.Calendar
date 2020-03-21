using Keesing.Calendar.Api.Models;

namespace Keesing.Calendar.Api.Services
{
    public interface ICalendarService
    {
        void AddEvent(Event @event);

        bool EditEvent(Event @event);

        bool DeleteEvent(int id);

        Event GetEventById(int id);

        Event GetEventByName(string name);

        Event[] GetAllEvents();
    }
}