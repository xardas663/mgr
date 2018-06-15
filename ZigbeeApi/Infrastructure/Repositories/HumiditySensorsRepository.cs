using Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure
{

    public class HumiditySensorsRepository : IHumiditySensorsRepository
    {
        private readonly ApplicationContext _dbContext;
        public HumiditySensorsRepository(ApplicationContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public void AddSensor(HumiditySensor sensor)
        {
            _dbContext.HumiditySensors.Add(sensor);
        }

        public void ChangeSensorRoom(Room room, HumiditySensor humidtySensor)
        {
            humidtySensor.Room = room;
        }

        public async Task<IEnumerable<HumiditySensor>> GetAll()
        => await _dbContext.HumiditySensors.ToListAsync();

        public async Task<HumiditySensor> GetSensorsByDomoticzID(string domoticzId)
        {
            var sensor = await _dbContext.HumiditySensors
                .Include(x=>x.Room)
                .Where(x => x.DomoticzId == domoticzId)
                .FirstOrDefaultAsync();
            return sensor;
        }

        public async Task<HumiditySensor> GetSensorsById(int Id)
        {
            var sensor = await _dbContext.HumiditySensors
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();
            return sensor;
        }

        public async Task<IEnumerable<HumiditySensor>> GetSensorsListByNames(IEnumerable<string> sensorNames)
        {
            var sensors = await _dbContext.HumiditySensors.Where(x => sensorNames.Contains(x.Name)).ToListAsync();
            return sensors;
        }
    }

}