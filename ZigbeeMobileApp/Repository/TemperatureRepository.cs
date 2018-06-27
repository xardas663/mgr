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
        public async Task<IEnumerable<TemperatureAvgDaily>> GetTemperatureAvgDaily(string sensorName)
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync($"http://zigbeeapi.azurewebsites.net/api/temperature/daily?sensorName={sensorName}");
                if (response.IsSuccessStatusCode)
                {
                    var temperatureJson = await response.Content.ReadAsStringAsync();
                    var temperature = JsonConvert.DeserializeObject<IEnumerable<TemperatureAvgDaily>>(temperatureJson);
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

        public async Task<IEnumerable<TemperatureAvgMonthly>> GetTemperatureAvgMonthly(string sensorName)
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync($"http://zigbeeapi.azurewebsites.net/api/temperature/monthly?sensorName={sensorName}");
                if (response.IsSuccessStatusCode)
                {
                    var temperatureJson = await response.Content.ReadAsStringAsync();
                    var temperature = JsonConvert.DeserializeObject<IEnumerable<TemperatureAvgMonthly>>(temperatureJson);
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

        public async Task<IEnumerable<TemperatureAvgYearly>> GetTemperatureAvgYearly(string sensorName)
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync($"http://zigbeeapi.azurewebsites.net/api/temperature/yearly?sensorName={sensorName}");
                if (response.IsSuccessStatusCode)
                {
                    var temperatureJson = await response.Content.ReadAsStringAsync();
                    var temperature = JsonConvert.DeserializeObject<IEnumerable<TemperatureAvgYearly>>(temperatureJson);
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