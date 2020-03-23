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

        /// <summary>
        /// The name of the event - must be unique.
        /// </summary>
        /// <example>Beach day</example>
        public string Name { get; set; }

        /// <summary>
        /// The time of the event needs to be in Epoch format.
        /// </summary>
        /// <example>1584990001</example>
        public int Time { get; set; }

        /// <summary>
        /// The location of the event.
        /// </summary>
        /// <example>Den haag beach</example>
        public string Location { get; set; }

        /// <summary>
        /// The members which are invited to the event.
        /// </summary>
        /// <example>The joker, Batman, wonderwoman, Superman, superwoman</example>
        public string Members { get; set; }

        /// <summary>
        /// The event organizer.
        /// </summary>
        /// <example>Batman</example>
        public string EventOrganizer { get; set; }

        /// <summary>
        /// Event id - being auto generated..
        /// </summary>
        /// <example>42</example>
        public int Id { get; private set; }

        private static readonly object _lockObj = new object();
        private static int _id = 0;
    }
}
