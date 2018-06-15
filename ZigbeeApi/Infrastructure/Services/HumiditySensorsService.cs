using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure
{

    public class HumiditySensorsService : IHumiditySensorsService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public HumiditySensorsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<HumiditySensor> GetSensorByDomoticzId(string domoticzId)
           => await _unitOfWork.HumiditySensorsRepository.GetSensorsByDomoticzID(domoticzId);

        public async Task AddSensor(HumiditySensor sensor)
        {
            _unitOfWork.HumiditySensorsRepository.AddSensor(sensor);
            await _unitOfWork.CommitChangesAsync();
        }

        public async Task ChangeSensorsRoom(int roomId, int humiditySensorId)
        {
            var room = await  _unitOfWork.RoomsRepository.GetRoomById(roomId);
            if(room != null)
            {
                var humiditySensor = await _unitOfWork.HumiditySensorsRepository.GetSensorsById(humiditySensorId);
                if(humiditySensor!= null)
                {
                    _unitOfWork.HumiditySensorsRepository.ChangeSensorRoom(room, humiditySensor);
                    await _unitOfWork.CommitChangesAsync();
                }               
            }
           
        }

        public async Task<IEnumerable<HumiditySensor>> GetAll()
            => await _unitOfWork.HumiditySensorsRepository.GetAll();

        public async Task<IEnumerable<HumiditySensor>> GetSensorsListByNames(IEnumerable<string> sensorNames)
            => await _unitOfWork.HumiditySensorsRepository.GetSensorsListByNames(sensorNames);
    }
}