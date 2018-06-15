using System.Collections.Generic;
using ZigbeeMobileApp.Model;
using System.Threading.Tasks;

namespace ZigbeeMobileApp.Repository
{

    public interface IRoomsRepository
    {
        Task<IEnumerable<Room>> GetAllRooms();
        Task EditRoom(Room room);
        Task AddRoom(Room room);

        Task DeleteRoom(string roomName);
    }
}