using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure
{

    public class TemperaturesRepository: ITemperaturesRepository
    {
        private readonly ApplicationContext _dbContext;
        public TemperaturesRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Temperature>> GetAll(int number, string date, string sensorName)
        {
            if (date == "all" && sensorName == "all")
            {
                return await _dbContext.Temperatures
               .OrderByDescending(x => x.Date)
               .Take(number)
               .Include(x => x.TemperatureSensor)
               .ThenInclude(x => x.Room)
               .ToListAsync();
            }

            else if (date == "all" && sensorName != "all")
            {
                return await _dbContext.Temperatures
              .OrderByDescending(x => x.Date)
              .Take(number)
              .Include(x => x.TemperatureSensor)
              .ThenInclude(x => x.Room)
              .Where(x => x.TemperatureSensor.Name == sensorName)
              .ToListAsync();
            }

            else if (date != "all" && sensorName == "all")
            {
                return await _dbContext.Temperatures
             .OrderByDescending(x => x.Date)
             .Take(number)
             .Include(x => x.TemperatureSensor)
             .ThenInclude(x => x.Room)
             .Where(x => x.Date.ToString("yyyy-MM-dd").Equals(date))
             .ToListAsync();
            }

            else if (date != "all" && sensorName != "all")
            {
                return await _dbContext.Temperatures
             .OrderByDescending(x => x.Date)
             .Take(number)
             .Include(x => x.TemperatureSensor)
             .ThenInclude(x => x.Room)
             .Where(x => x.Date.ToString("yyyy-MM-dd").Equals(date))
             .Where(x => x.TemperatureSensor.Name == sensorName)
             .ToListAsync();
            }
            else { return null; }

        }

        public void AddTemperature(Temperature temperature)
            => _dbContext.Temperatures.Add(temperature);

        public async Task<IEnumerable<Temperature>> GetTemperaturesFromGivenDay(string dateTime, string sensorName)
            => await _dbContext.Temperatures
            .Where(x => x.Date.ToString("yyyy-MM-dd").Equals(dateTime))
            .Where(x => x.TemperatureSensor.Name == sensorName)
            .ToListAsync();

    }
}