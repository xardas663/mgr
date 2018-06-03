using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using System.Net.Http;
using Newtonsoft.Json;

namespace ZigbeeMobileApp.Repository
{

    public class TemperatureSensorsReposiotory : ITemeratureSensorsRepository
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
    }
}