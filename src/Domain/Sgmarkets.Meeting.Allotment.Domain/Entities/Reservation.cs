using System;

namespace Sgmarkets.Meeting.Allotment.Domain.Entities
{
    public class Reservation
    {
        /// <summary>
        /// Gets, sets the name of organizer of the meeting
        /// </summary>
        public string Organizer { get; set; }

        /// <summary>
        /// Gets,sets day of meeting
        /// </summary>
        public DateTime Day { get; set; }

        /// <summary>
        /// Gets, sets the time of begenning of the meeting
        /// </summary>
        public TimeSpan BeginTime { get; set; }

        /// <summary>
        /// Gets, sets the time of begenning of the meeting
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// Gets, set the meeting room
        /// </summary>
        public Room Room { get; set; }
    }
}
