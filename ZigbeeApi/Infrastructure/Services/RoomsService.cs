using Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure
{

    public class RoomsService : IRoomsService
    {
        private readonly ITemperatureSensorsService _temperatureSensorsService;
        private readonly IHumiditySensorsService _humiditySensorsService;
        private readonly IUnitOfWork _unitOfWork;

        public RoomsService(ITemperatureSensorsService temperatureSensorsService, IHumiditySensorsService humiditySensorsService, IUnitOfWork unitOfWork)
        {
            _temperatureSensorsService = temperatureSensorsService;
            _humiditySensorsService = humiditySensorsService;
            _unitOfWork = unitOfWork;
        }
        public async Task AddRoom(Room room)
        {
            _unitOfWork.RoomsRepository.AddRoom(room);
            await _unitOfWork.CommitChangesAsync();
        }

        public async Task AddSensorToRoom(string roomName, SensorType type, string domoticzId)
        {
            var room = await GetRoom(roomName);
            if(room != null)
            {
                if (type == SensorType.Humidity)
                {
                    var sensor = await _humiditySensorsService.GetSensorByDomoticzId(domoticzId);
                    if (sensor != null) room.HumiditySensors.Add(sensor);
                    await _unitOfWork.CommitChangesAsync();
                }

                else if (type == SensorType.Temparature)
                {
                    var sensor = await _temperatureSensorsService.GetSensorsByDomoticzIds(domoticzId);
                    if (sensor != null) room.TemperatureSensors.Add(sensor);
                    await _unitOfWork.CommitChangesAsync();
                }
            }           
        }

        public async Task EditRoom(Room room)
        {
            var dbRoom = await _unitOfWork.RoomsRepository.GetRoomById(room.Id);
            var humiditySensorNames = room.HumiditySensors.Select(x => x.Name);
            var temperatureSensorNames = room.TemperatureSensors.Select(x => x.Name);

            var dbHumiditySensors = await _humiditySensorsService.GetSensorsListByNames(humiditySensorNames);
            var dbTemperatureSensors = await _temperatureSensorsService.GetSensorsListByNames(temperatureSensorNames);

            room.HumiditySensors = dbHumiditySensors.ToList();
            room.TemperatureSensors = dbTemperatureSensors.ToList();


            if (dbRoom != null)
            {
                _unitOfWork.RoomsRepository.EditRoom(dbRoom, room);
                await _unitOfWork.CommitChangesAsync();
            }
        }

        public async Task<IEnumerable<Room>> GetAllRooms()
            => await _unitOfWork.RoomsRepository.GetAll();

        public async Task<Room> GetRoom(string roomName)
            => await _unitOfWork.RoomsRepository.GetRoom(roomName);

        public async Task DeleteRoom (string roomName)
        {
            var room = await _unitOfWork.RoomsRepository.GetRoom(roomName);
            _unitOfWork.RoomsRepository.DeleteRoom(room);
            await _unitOfWork.CommitChangesAsync();
        }
        

    }



}