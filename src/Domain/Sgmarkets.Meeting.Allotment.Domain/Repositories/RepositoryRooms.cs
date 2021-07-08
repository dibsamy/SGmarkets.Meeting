using Sgmarkets.Meeting.Allotment.Domain.Entities;
using Sgmarkets.Meeting.Allotment.Domain.Repositories.Interfaces;
using System.Collections.Generic;


namespace Sgmarkets.Meeting.Allotment.Domain.Repositories
{
    public class RepositoryRooms : IRepositoryRooms
    {
        private readonly IList<Room> _rooms;
        public RepositoryRooms(int nbromm = 10)
        {
            _rooms = new List<Room>();
            int i = 0;
            do
            {
                _rooms.Add(new Room { Name = $"room{i}" });
            } while (++i < nbromm);
        }

        public IEnumerable<Room> Items()
            => _rooms;
    }
}
