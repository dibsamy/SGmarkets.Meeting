using Sgmarkets.Meeting.Allotment.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Sgmarkets.Meeting.Allotment.Domain.Repositories.Interfaces
{
    public interface IRepositoryReservations
    {
        IEnumerable<Slot> GetSlots(string room, DateTime day);
        IEnumerable<Reservation> GetReservations(string room, DateTime day);
        Reservation Find(DateTime day, TimeSpan beginTime, TimeSpan endTime, string roomName);
        void Add(Reservation reservation);
        void Delete(Reservation reservation);
    }
}
