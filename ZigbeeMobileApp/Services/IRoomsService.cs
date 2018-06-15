using ZigbeeMobileApp.Model;
using System.Threading.Tasks;

namespace ZigbeeMobileApp.Services
{

    public interface IRoomsService
    {
        Task EditRoom(Room room);
        Task AddRoom(Room room);
        Task DeleteRoom(string roomName);
    }
}