using System.Collections.Generic;
using System.Threading.Tasks;
using Core;

namespace Infrastructure
{
    public interface IRoomsRepository 
    {
        Task<Room> GetRoom(string roomName);

        Task<IEnumerable<Room>> GetAll();

        void AddRoom(Room room);

        Task<Room> GetRoomById(int Id);
        void EditRoom(Room dbRoom, Room room);
        void DeleteRoom(Room room);
    }
}