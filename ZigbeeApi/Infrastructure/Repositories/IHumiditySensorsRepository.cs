using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IHumiditySensorsRepository
    {
        Task<HumiditySensor> GetSensorsByDomoticzID(string domoticzId);

        void AddSensor(HumiditySensor sensor);
        void ChangeSensorRoom(Room room, HumiditySensor humidtySensor);
        Task<HumiditySensor> GetSensorsById(int Id);
        Task<IEnumerable<HumiditySensor>> GetAll();
        Task<IEnumerable<HumiditySensor>> GetSensorsListByNames(IEnumerable<string> sensorNames);
    }
}