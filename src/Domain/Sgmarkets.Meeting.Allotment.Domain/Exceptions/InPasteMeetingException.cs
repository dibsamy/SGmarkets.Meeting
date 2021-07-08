using Sgmarkets.Meeting.Allotment.Domain.Entities;
using System;
using System.Runtime.Serialization;

namespace Sgmarkets.Meeting.Allotment.Domain.Exceptions
{
    public class InPasteMeetingException : DomainException
    {
        public InPasteMeetingException(Reservation reservation)
           : this("Unable to create a meeting in the past.")
        {
            _reservation = reservation;
        }

        public InPasteMeetingException(string message) : base(message)
        {
        }

        public InPasteMeetingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InPasteMeetingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}