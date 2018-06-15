using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface ITemperatureSensorsRepository
    {
        Task<TemperatureSensor> GetSensorsByDomoticzID(string domoticzId);

        void AddSensor(TemperatureSensor sensor);
        void ChangeSensorRoom(Room room, TemperatureSensor temperatureSensor);

        Task<TemperatureSensor> GetSensorsById(int Id);
        Task<IEnumerable<TemperatureSensor>> GetAll();
        Task<IEnumerable<TemperatureSensor>> GetSensorsListByNames(IEnumerable<string> sensorNames);
    }
}