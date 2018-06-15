using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{

    public class RoomsReposiotry : IRoomsRepository
    {
        private readonly ApplicationContext _dbContext;

        public RoomsReposiotry(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddRoom(Room room)
            => _dbContext.Add(room);

        public void DeleteRoom(Room room)
            => _dbContext.Rooms.Remove(room);

        public void EditRoom(Room dbRoom, Room room)
        {
            dbRoom.Description = room.Description;
            dbRoom.ExpectedHumidity = room.ExpectedHumidity;
            dbRoom.ExpectedTemperature = room.ExpectedTemperature;
            dbRoom.Name = room.Name;
            dbRoom.MaxHumidity = room.MaxHumidity;
            dbRoom.MaxTemperature = room.MaxTemperature;
            dbRoom.MinHumidity = room.MinHumidity;
            dbRoom.MinTemperature = room.MinTemperature;
            dbRoom.HumiditySensors = room.HumiditySensors;
            dbRoom.TemperatureSensors = room.TemperatureSensors;
        }

        public async Task<IEnumerable<Room>> GetAll()
            => await _dbContext.Rooms.Select(x => new Room
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ExpectedHumidity = x.ExpectedHumidity,
                ExpectedTemperature = x.ExpectedTemperature,
                MaxHumidity=x.MaxHumidity,
                MaxTemperature=x.MaxTemperature,
                MinHumidity=x.MinHumidity,
                MinTemperature=x.MaxTemperature,
                HumiditySensors = x.HumiditySensors.Select(h => new HumiditySensor()
                {
                    DomoticzId = h.DomoticzId,
                    Name = h.Name,
                    Desription = h.Desription,
                    Humidity = h.Humidity.TakeLast(1).ToList()
                }).ToList(),
                TemperatureSensors=x.TemperatureSensors.Select(t=> new TemperatureSensor()
                {
                    DomoticzId = t.DomoticzId,
                    Name = t.Name,
                    Desription = t.Desription,
                    Temperatures = t.Temperatures.TakeLast(1).ToList()
                }).ToList()                                
            })
            .ToListAsync();            

        public async Task<Room> GetRoom(string roomName)
            => await _dbContext.Rooms.Where(x => x.Name == roomName)
                .FirstOrDefaultAsync();

        public async Task<Room> GetRoomById(int Id)
            => await _dbContext.Rooms.Where(x => x.Id == Id)
                .FirstOrDefaultAsync();
    }
}