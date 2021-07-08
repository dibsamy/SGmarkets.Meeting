using Sgmarkets.Meeting.Allotment.Api.Models;
using Sgmarkets.Meeting.Allotment.Domain.Entities;
using System;

namespace Sgmarkets.Meeting.Allotment.Api.Mappings
{
    public static class ReservationMappings
    {

        public static Reservation MapToEntity(this ReservationModel model)
        {
            if (model == null)
                return null;

            return new Reservation
            {
                BeginTime = TimeSpan.Parse(model.BeginTime),
                EndTime = TimeSpan.Parse(model.EndTime),
                Day = model.Day.Date,
                Organizer = model.Organizer,
            };
        }


        public static ReservationModel MapToModel(this Reservation entity)
        {
            if (entity == null)
                return null;

            return new ReservationModel
            {
                BeginTime = entity.BeginTime.ToString(),
                EndTime = entity.EndTime.ToString(),
                Day = entity.Day.Date,
                Organizer = entity.Organizer,
                RoomName = entity.Room.Name
            };
        }

    }
}
