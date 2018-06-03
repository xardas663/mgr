using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using System.Net.Http;
using Newtonsoft.Json;

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
    }
}