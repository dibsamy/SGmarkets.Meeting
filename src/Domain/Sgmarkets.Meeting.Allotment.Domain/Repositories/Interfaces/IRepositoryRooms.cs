using Sgmarkets.Meeting.Allotment.Domain.Entities;
using System.Collections.Generic;

namespace Sgmarkets.Meeting.Allotment.Domain.Repositories.Interfaces
{
    public interface IRepositoryRooms
    {
        IEnumerable<Room> Items();
    }
}
