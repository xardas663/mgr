using System.Collections.Generic;
using System.Threading.Tasks;
using ZigbeeMobileApp.Model;
using System.Net.Http;
using Newtonsoft.Json;

namespace ZigbeeMobileApp.Repository
{

    public class TemperatureSensorsRepository : ITemeratureSensorsRepository
    {
        public async Task<IEnumerable<TemperatureSensor>> GetAll()
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"http://zigbeeapi.azurewebsites.net/api/temperaturesensors");
            if (response.IsSuccessStatusCode)
            {
                var temperatureSensorsJson = await response.Content.ReadAsStringAsync();
                var temperatureSensors = JsonConvert.DeserializeObject<IEnumerable<TemperatureSensor>>(temperatureSensorsJson);
                return temperatureSensors;
            }
            else
            {
                return null;
            }
        }

        public async Task<Room> GetRoomForGivenSensorName(string sensorName)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"http://zigbeeapi.azurewebsites.net/api/temperaturesensors/room?sensorName={sensorName}");
            if (response.IsSuccessStatusCode)
            {
                var roomJson = await response.Content.ReadAsStringAsync();
                var room = JsonConvert.DeserializeObject<Room>(roomJson);
                return room;
            }
            else
            {
                return null;
            }
        }
    }
}