using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using ZigbeeMobileApp.Model;

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

        public async Task<IEnumerable<HumidityAvgDaily>> GetHumidityAvgDaily(string sensorName)
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync($"http://zigbeeapi.azurewebsites.net/api/humidity/daily?sensorName={sensorName}");
                if (response.IsSuccessStatusCode)
                {
                    var humidityJson = await response.Content.ReadAsStringAsync();
                    var humidity = JsonConvert.DeserializeObject<IEnumerable<HumidityAvgDaily>>(humidityJson);
                    return humidity;
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

        public async Task<IEnumerable<HumidityAvgMonthly>> GetHumidityAvgMonthly(string sensorName)
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync($"http://zigbeeapi.azurewebsites.net/api/humidity/monthly?sensorName={sensorName}");
                if (response.IsSuccessStatusCode)
                {
                    var humidityJson = await response.Content.ReadAsStringAsync();
                    var humidity = JsonConvert.DeserializeObject<IEnumerable<HumidityAvgMonthly>>(humidityJson);
                    return humidity;
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

        public async Task<IEnumerable<HumidityAvgYearly>> GetHumidityAvgYearly(string sensorName)
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync($"http://zigbeeapi.azurewebsites.net/api/humidity/yearly?sensorName={sensorName}");
                if (response.IsSuccessStatusCode)
                {
                    var humidityJson = await response.Content.ReadAsStringAsync();
                    var humidity = JsonConvert.DeserializeObject<IEnumerable<HumidityAvgYearly>>(humidityJson);
                    return humidity;
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