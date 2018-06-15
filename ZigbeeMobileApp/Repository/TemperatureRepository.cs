using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ZigbeeMobileApp.Model;
using System;

namespace ZigbeeMobileApp.Repository
{
    public class TemperatureRepository : ITemperatureRepository
    {
        public async Task<IEnumerable<Temperature>> GetTemperatures(int number, string date, string sensorName)
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync($"http://zigbeeapi.azurewebsites.net/api/temperature?number={number}&date={date}&sensorname={sensorName}");
                if (response.IsSuccessStatusCode)
                {
                    var temperatureJson = await response.Content.ReadAsStringAsync();
                    var temperature = JsonConvert.DeserializeObject<IEnumerable<Temperature>>(temperatureJson);
                    return temperature;
                }

                else
                {
                    return null;
                }
            }
            catch (System.Exception e)
            {

                throw e;
            }
           
            
        }      
    }
}