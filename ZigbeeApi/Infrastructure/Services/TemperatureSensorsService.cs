using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure
{

    public class TemperatureSensorsService : ITemperatureSensorsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TemperatureSensorsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddSensor(TemperatureSensor sensor)
        {
            _unitOfWork.TemperatureSensorsRepository.AddSensor(sensor);
            await _unitOfWork.CommitChangesAsync();
        }

        public async Task ChangeSensorsRoom(int roomId, int temperatureSensorId)
        {

            var room = await _unitOfWork.RoomsRepository.GetRoomById(roomId);
            if (room != null)
            {
                var temperatureSensor = await _unitOfWork.TemperatureSensorsRepository.GetSensorsById(temperatureSensorId);
                if (temperatureSensor != null)
                {
                    _unitOfWork.TemperatureSensorsRepository.ChangeSensorRoom(room, temperatureSensor);
                    await _unitOfWork.CommitChangesAsync();
                }
            }                   
        }

        public async Task<IEnumerable<TemperatureSensor>> GetAll()
            => await _unitOfWork.TemperatureSensorsRepository.GetAll();

        public Task<TemperatureSensor> GetSensorsByDomoticzIds(string domoticzId)
            => _unitOfWork.TemperatureSensorsRepository.GetSensorsByDomoticzID(domoticzId);

        public async Task<IEnumerable<TemperatureSensor>> GetSensorsListByNames(IEnumerable<string> sensorNames)
            => await _unitOfWork.TemperatureSensorsRepository.GetSensorsListByNames(sensorNames);
    }
}