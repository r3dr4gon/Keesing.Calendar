using Keesing.Calendar.Api.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Keesing.Calendar.Test
{
    class Helpers
    {
        public static Event FakeEventGen()
        {
            var randomNum = random.Next();

            return new Event
            {
                Name = $"Testing party {randomNum}",
                Time = randomNum,
                Location = "Here",
                Members = "All by myself",
                EventOrganizer = "Myself"
            };
        }

        public static Event GetBobPartyEvent()
        {
            return bobPartyEvent;
        }

        public static Event GetBobDrinksEvent()
        {
            return bobDrinksEvent;
        }

        public static Event GetJoopOutEvent()
        {
            return joopOutEvent;
        }

        private static readonly Event bobPartyEvent = new Event
        {
            Name = $"Party at Bob",
            Time = 1,
            Location = "Bob House",
            Members = "Bob",
            EventOrganizer = "Bob"
        };

        private static readonly Event bobDrinksEvent = new Event
        {
            Name = $"Drinks at Bob",
            Time = 2,
            Location = "Bob House",
            Members = "Bob",
            EventOrganizer = "Bob"
        };

        private static readonly Event joopOutEvent = new Event
        {
            Name = $"Going out with Joop",
            Time = 3,
            Location = "Out",
            Members = "Joop",
            EventOrganizer = "Joop"
        };


        private static readonly Random random = new Random();
    }
}

