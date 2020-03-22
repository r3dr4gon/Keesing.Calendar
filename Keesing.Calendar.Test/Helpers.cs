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
                Time = 0,
                Location = "Here",
                Members = "All by myself",
                EventOrganizer = "Myself"
            };
        }

        private static readonly Random random = new Random();
    }
}

