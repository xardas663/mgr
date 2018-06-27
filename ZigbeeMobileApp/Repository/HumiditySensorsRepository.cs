using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using ZigbeeMobileApp.Model;

namespace ZigbeeMobileApp.Repository
{

    public class HumiditySensorsRepository : IHumiditySensorsRepository
    {
        public async Task<IEnumerable<HumiditySensor>> GetAll()
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"http://zigbeeapi.azurewebsites.net/api/humiditysensors");
            if (response.IsSuccessStatusCode)
            {
                var humiditySensorsJson = await response.Content.ReadAsStringAsync();
                var temperatureSensors = JsonConvert.DeserializeObject<IEnumerable<HumiditySensor>>(humiditySensorsJson);
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
            var response = await client.GetAsync($"http://zigbeeapi.azurewebsites.net/api/humiditysensors/room?sensorName={sensorName}");
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