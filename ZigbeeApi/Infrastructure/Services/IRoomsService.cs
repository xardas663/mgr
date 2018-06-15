using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure
{

    public interface IRoomsService
    {
        Task<IEnumerable<Room>> GetAllRooms();
        Task<Room> GetRoom(string roomName);
        Task AddRoom(Room room);
        Task AddSensorToRoom(string roomName, SensorType type, string sensorName);
        Task EditRoom(Room room);
        Task DeleteRoom(string roomName);
    }
}