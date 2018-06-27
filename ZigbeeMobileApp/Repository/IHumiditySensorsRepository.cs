using System.Collections.Generic;
using System.Threading.Tasks;
using ZigbeeMobileApp.Model;

namespace ZigbeeMobileApp.Repository
{

    public interface IHumiditySensorsRepository
    {
        Task<IEnumerable<HumiditySensor>> GetAll();
        Task<Room> GetRoomForGivenSensorName(string sensorName);
    }
}