using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure
{

    public interface IHumiditySensorsService
    {
        Task<HumiditySensor> GetSensorByDomoticzId(string domoticzId);
        Task AddSensor(HumiditySensor sensor);

        Task<IEnumerable<HumiditySensor>> GetAll();
        Task ChangeSensorsRoom(int roomId, int humiditySensorId);
        Task<IEnumerable<HumiditySensor>> GetSensorsListByNames(IEnumerable<string> sensorNames);
    }

}