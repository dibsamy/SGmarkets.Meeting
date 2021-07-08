using Sgmarkets.Meeting.Allotment.Domain.Entities;
using System;
using System.Runtime.Serialization;

namespace Sgmarkets.Meeting.Allotment.Domain.Exceptions
{

    [Serializable]
    public abstract class DomainException : Exception
    {
        protected Reservation _reservation;
        protected Reservation Reservation
        {
            get
            {
                return _reservation;
            }
        }

        public DomainException(string message) : base(message)
        {
        }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
