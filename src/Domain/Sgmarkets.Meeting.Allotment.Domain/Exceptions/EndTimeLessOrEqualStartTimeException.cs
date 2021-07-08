using Sgmarkets.Meeting.Allotment.Domain.Entities;
using System;
using System.Runtime.Serialization;

namespace Sgmarkets.Meeting.Allotment.Domain.Exceptions
{
    public class EndTimeLessOrEqualStartTimeException : DomainException
    {
        public EndTimeLessOrEqualStartTimeException(Reservation reservation)
            : this("The beginning time must be greater than the end time.")
        {
            _reservation = reservation;
        }

        public EndTimeLessOrEqualStartTimeException(string message) : base(message)
        {
        }

        public EndTimeLessOrEqualStartTimeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EndTimeLessOrEqualStartTimeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}