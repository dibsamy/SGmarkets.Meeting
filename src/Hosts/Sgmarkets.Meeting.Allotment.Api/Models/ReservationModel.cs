using System;
using System.ComponentModel.DataAnnotations;

namespace Sgmarkets.Meeting.Allotment.Api.Models
{
    public class ReservationModel
    {
        [Required]
        public string Organizer { get; set; }

        [Required]
        public DateTime Day { get; set; }

        [Required]
        public string RoomName { get; set; }

        [Required]
        public string BeginTime { get; set; }

        [Required]
        public string EndTime { get; set; }

    }
}
