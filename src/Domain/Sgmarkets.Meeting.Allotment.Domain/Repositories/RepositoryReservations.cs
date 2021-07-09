using Sgmarkets.Meeting.Allotment.Domain.Entities;
using Sgmarkets.Meeting.Allotment.Domain.Exceptions;
using Sgmarkets.Meeting.Allotment.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sgmarkets.Meeting.Allotment.Domain.Repositories
{
    public class RepositoryReservations : IRepositoryReservations
    {
        private int nbSlots = 24;
        private readonly IDictionary<string, List<Reservation>> _reservations;

        public RepositoryReservations()
        {
            _reservations = new Dictionary<string, List<Reservation>>();
        }

        public void Add(Reservation reservation)
        {
            if (reservation == null)
                throw new ArgumentNullException(nameof(reservation), "The reservation cannot be null.");

            if (reservation.Room == null)
                throw new ArgumentNullException(nameof(reservation.Room), "The room of Reservation cannot be null.");

            var inPast = DateTime.Compare(reservation.Day.Date, DateTime.Now.Date);
            if (inPast < 0)
                throw new InPasteMeetingException(reservation);

            var endTimeLessOrEqualStartTime = TimeSpan.Compare(reservation.EndTime, reservation.BeginTime);
            if (endTimeLessOrEqualStartTime <= 0)
                throw new EndTimeLessOrEqualStartTimeException(reservation);

            //Generate day's key
            var key = GenerateKey(reservation.Room.Name, reservation.Day);

            //If this is the first meeting for this day
            if (!_reservations.ContainsKey(key))
            {
                _reservations[key] = new List<Reservation>() { reservation };
                return;
            }

            var reservations = _reservations[key];

            var isOverlap = IsOverlap(reservations, reservation);

            if (isOverlap)
                throw new OverlapMeetingException(reservation);

            reservations.Add(reservation);
        }

        public void Delete(Reservation reservation)
        {
            var key = GenerateKey(reservation.Room.Name, reservation.Day);

            if (!_reservations.ContainsKey(key))
            {
                return;
            }

            var reservations = _reservations[key];

            reservations.Remove(reservation);
        }

        public Reservation Find(DateTime day, TimeSpan beginTime, TimeSpan endTime, string roomName)
        {
            var key = GenerateKey(roomName, day);

            if (!_reservations.ContainsKey(key))
            {
                return null;
            }

            bool predicat(Reservation r) => r.Room.Name == roomName
                                                  && TimeSpan.Compare(r.BeginTime, beginTime) == 0
                                                  && TimeSpan.Compare(r.EndTime, endTime) == 0;

            var reservation = _reservations[key].FirstOrDefault(predicat);

            return reservation;

        }

        public IEnumerable<Reservation> GetReservations(string room, DateTime day)
        {
            var key = GenerateKey(room, day);

            if (!_reservations.ContainsKey(key))
            {
                return new List<Reservation>();
            }
            return _reservations[key];
        }


        public IEnumerable<Slot> GetSlots(string room, DateTime day)
        {
            List<Slot> slots;

            var key = GenerateKey(room, day);

            if (!_reservations.ContainsKey(key))
            {
                slots = new List<Slot>();
                for (var i = 1; i <= nbSlots; ++i)
                    slots.Add(new Slot(new TimeSpan(i % nbSlots, 0, 0), true));
            }
            else
            {
                slots = BuidOccupedSolts(_reservations[key]);
                var i = 1;
                var start = new TimeSpan(i, 0, 0);
                do
                {
                    var slot = slots.FirstOrDefault(s => TimeSpan.Compare(s.Start, start) == 0);
                    if (slot == null)
                        slots.Add(new Slot(start, true));

                    start = new TimeSpan(++i % nbSlots, 0, 0);

                } while (i <= nbSlots);
            }

            return slots;
        }

        #region : Utilities
        private string GenerateKey(string room, DateTime day) => $"{room}_{day.ToString("dd_MM_yyyy")}";

        private bool IsOverlap(IList<Reservation> meetings, Reservation meeting)
        {
            var slots = new List<Slot>();

            foreach (var a in meetings)
            {
                var start = a.BeginTime;
                do
                {
                    //Gets end of meeting
                    var slot = slots.FirstOrDefault(e => TimeSpan.Compare(e.Start, start) == 0 && e.Free);
                    if (slot != null)
                    {
                        slot.Free = false;
                    }
                    else
                    {
                        slot = new Slot(start, TimeSpan.Compare(start, a.EndTime) == 0);
                        slots.Add(slot);
                    }
                    start = start.Add(new TimeSpan(1, 0, 0));
                } while (start <= a.EndTime);
            }

            var isOverlap = false;

            var beginTime = meeting.BeginTime;
            do
            {
                var overlap = slots.FirstOrDefault(sl => sl.Start == beginTime && !sl.Free);
                if (overlap != null)
                {
                    isOverlap = true;
                    break;
                }
                beginTime = beginTime.Add(new TimeSpan(1, 0, 0));
            } while (beginTime < meeting.EndTime);

            return isOverlap;
        }

        private List<Slot> BuidOccupedSolts(IList<Reservation> meetings)
        {
            var slots = new List<Slot>();

            foreach (var a in meetings)
            {
                var start = a.BeginTime;
                do
                {
                    //Gets end of meeting
                    var slot = slots.FirstOrDefault(e => TimeSpan.Compare(e.Start, start) == 0 && e.Free);
                    if (slot != null)
                    {
                        slot.Free = false;
                    }
                    else
                    {
                        slot = new Slot(start, TimeSpan.Compare(start, a.EndTime) == 0);
                        slots.Add(slot);
                    }
                    start = start.Add(new TimeSpan(1, 0, 0));
                } while (start <= a.EndTime);
            }

            return slots;
        }
        #endregion
    }
}
