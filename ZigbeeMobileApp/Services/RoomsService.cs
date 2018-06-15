using ZigbeeMobileApp.Model;
using System.Threading.Tasks;
using ZigbeeMobileApp.Repository;

namespace ZigbeeMobileApp.Services
{
    public class RoomsService : IRoomsService
    {              
     
        public async Task AddRoom(Room room)
        {
            var _roomsRepository = new RoomsRepository();
            await _roomsRepository.AddRoom(room);
        }

        public async Task EditRoom(Room room)
        {
            var _roomsRepository = new RoomsRepository();
            await _roomsRepository.EditRoom(room);
        }

        public async Task DeleteRoom(string roomName)
        {
            var _roomsRepository = new RoomsRepository();
            await _roomsRepository.DeleteRoom(roomName);
        }
    }
}