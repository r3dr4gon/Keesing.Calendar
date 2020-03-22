using System;
using System.ComponentModel.DataAnnotations;

namespace Keesing.Calendar.Api.Models
{
    public class Event
    {
        public Event()
        {
            lock (_lockObj)
            {
                this.Id = _id++;
            }
        }

        public string Name { get; set; }
        public int Time { get; set; }
        public string Location { get; set; }
        public string Members { get; set; }
        public string EventOrganizer { get; set; }
        public int Id { get; private set; }

        private static readonly object _lockObj = new object();
        private static int _id = 0;
    }
}
