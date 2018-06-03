using System.Collections.Generic;
using Core;
using System.Threading.Tasks;

namespace ZigbeeMobileApp.Repository
{

    public interface IRoomsRepository
    {
        Task<IEnumerable<Room>> GetAllRooms();
        Task EditRoom(Room room);
        Task AddRoom(Room room);
    }
}