using System.Collections.Generic;
using Core;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;

namespace ZigbeeMobileApp.Repository
{

    public class HumidityRepository : IHumidityRepository
    {
        public async Task<IEnumerable<Humidity>> GetHumidity(int number,string date, string sensorName)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"http://zigbeeapi.azurewebsites.net/api/humidity?number={number}&date={date}&sensorname={sensorName}");
            if (response.IsSuccessStatusCode)
            {
                var humidityJson = await response.Content.ReadAsStringAsync();
                var humidity = JsonConvert.DeserializeObject<IEnumerable<Humidity>>(humidityJson);
                return humidity;
            }
            else
            {
                return null; 
            }           

        }

        public async Task<IEnumerable<Humidity>> GetHumidityForGivenDay(DateTime dateTime)
        {
            var client = new HttpClient();
            var date = dateTime.ToString("yyyy-MM-dd");
            var response = await client.GetAsync($"http://zigbeeapi.azurewebsites.net/api/humidity/plot?dateTime={date}");
            if (response.IsSuccessStatusCode)
            {
                var humidityJson = await response.Content.ReadAsStringAsync();
                var humidity = JsonConvert.DeserializeObject<IEnumerable<Humidity>>(humidityJson);
                return humidity;
            }
            else
            {
                return null;
            }

        }
    }
}