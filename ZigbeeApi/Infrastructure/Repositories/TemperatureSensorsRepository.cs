using Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Infrastructure
{

    public class TemperatureSensorsRepository : ITemperatureSensorsRepository
    {
        private readonly ApplicationContext _dbContext;
        public TemperatureSensorsRepository(ApplicationContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public void AddSensor(TemperatureSensor sensor)
        {
            _dbContext.TemperatureSensors.Add(sensor);
        }

        public void ChangeSensorRoom(Room room, TemperatureSensor temperatureSensor)
        {
            temperatureSensor.Room = room;
        }

        public async Task<IEnumerable<TemperatureSensor>> GetAll()
            => await _dbContext.TemperatureSensors.ToListAsync();

        public async Task<TemperatureSensor> GetSensorsByDomoticzID(string domoticzId)
        {           
            var sensors = await _dbContext.TemperatureSensors
                .Include(x=>x.Room)
                .Where(x => x.DomoticzId == domoticzId)
                .FirstOrDefaultAsync();
            return sensors;            
        }

        public async Task<TemperatureSensor> GetSensorsById(int Id)
        {
            var sensor = await _dbContext.TemperatureSensors
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();
            return sensor;
        }

        public async Task<IEnumerable<TemperatureSensor>> GetSensorsListByNames(IEnumerable<string> sensorNames)
        {
            var sensors = await _dbContext.TemperatureSensors.Where(x => sensorNames.Contains(x.Name)).ToListAsync();
            return sensors;
        }
    }
}