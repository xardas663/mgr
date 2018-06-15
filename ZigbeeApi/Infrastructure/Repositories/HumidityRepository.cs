using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure
{

    public class HumidityRepository : IHumidityRepository
    {
        private readonly ApplicationContext _dbContext;
        public HumidityRepository(ApplicationContext dbContext) 
        {
            _dbContext = dbContext;
        }
    

        public void AddHumidity(Humidity humidity)
            => _dbContext.Humidity.Add(humidity);

        public async Task<int> GetHumidityNumber()
            => await _dbContext.Humidity.CountAsync();

        public async Task<IEnumerable<Humidity>> GetHumidityFromGivenDay(string dateTime, string sensorName)
            => await _dbContext.Humidity
            .Where(x => x.Date.ToString("yyyy-MM-dd").Equals(dateTime))
            .Where(x => x.HumiditySensor.Name == sensorName)
                .ToListAsync();

        public async Task<IEnumerable<Humidity>> GetAll(int number, string date, string sensorName)
        {
            if(date == "all" && sensorName == "all")
            {
                 return await _dbContext.Humidity
                .OrderByDescending(x => x.Date)
                .Take(number)
                .Include(x => x.HumiditySensor)
                .ThenInclude(x => x.Room)
                .ToListAsync();
            }

            else if(date=="all" && sensorName != "all")
            {
                return await _dbContext.Humidity
              .OrderByDescending(x => x.Date)
              .Take(number)
              .Include(x => x.HumiditySensor)
              .ThenInclude(x => x.Room)
              .Where(x => x.HumiditySensor.Name == sensorName)
              .ToListAsync();
            }

            else if (date != "all" && sensorName == "all")
            {
                return await _dbContext.Humidity
             .OrderByDescending(x => x.Date)
             .Take(number)
             .Include(x => x.HumiditySensor)
             .ThenInclude(x => x.Room)
             .Where(x => x.Date.ToString("yyyy-MM-dd").Equals(date))
             .ToListAsync();
            }

            else if(date != "all" && sensorName != "all")
            {
                return await _dbContext.Humidity
             .OrderByDescending(x => x.Date)
             .Take(number)
             .Include(x => x.HumiditySensor)
             .ThenInclude(x => x.Room)
             .Where(x=>x.Date.ToString("yyyy-MM-dd").Equals(date))
             .Where(x=>x.HumiditySensor.Name==sensorName)
             .ToListAsync();
            }
            else { return null; }             
        }
    }
}