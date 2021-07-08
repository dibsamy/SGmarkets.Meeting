using System.Collections.Generic;

namespace Sgmarkets.Meeting.Allotment.Domain.Entities
{
    public class Room
    {

        public Room()
        {
            this.Reservations = new List<Reservation>();
        }

        public string Name { get; set; }

        public IEnumerable<Reservation> Reservations { get; set; }
    }
}
