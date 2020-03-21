using Keesing.Calendar.Api.Models;

namespace Keesing.Calendar.Api.Services
{
    public interface ICalendarService
    {
        void AddEvent(Event @event);

        void EditEvent(Event @event);

        void DeleteEvent(int id);

        Event GetEventById(int id);

        Event GetEventByName(string name);

        Event[] GetAllEvents();
    }
}