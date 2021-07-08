using Sgmarkets.Meeting.Allotment.Domain.Entities;
using System;
using System.Runtime.Serialization;

namespace Sgmarkets.Meeting.Allotment.Domain.Exceptions
{

    public class OverlapMeetingException : DomainException
    {
        public OverlapMeetingException(Reservation reservation)
            : this("The reservation is overlap with another reservation.")
        {
            _reservation = reservation;
        }

        public OverlapMeetingException(string message) : base(message)
        {
        }

        public OverlapMeetingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OverlapMeetingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}