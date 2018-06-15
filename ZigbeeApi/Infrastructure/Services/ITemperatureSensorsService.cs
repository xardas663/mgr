using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure
{

    public interface ITemperatureSensorsService
    {
        Task<TemperatureSensor> GetSensorsByDomoticzIds(string domoticzId);
        Task AddSensor(TemperatureSensor sensor);

        Task<IEnumerable<TemperatureSensor>> GetAll();

        Task ChangeSensorsRoom(int roomId, int temperatureSensorId);
        Task<IEnumerable<TemperatureSensor>> GetSensorsListByNames(IEnumerable<string> sensorNames);
    }
}