using Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure
{    
    public interface IHumidityService
    {
        Task<IEnumerable<Humidity>> GetHumidity(int number, string date, string sensorName);
        Task AddHumidity(Humidity humidity);
        Task<int> GetHumidityNumber();
        Task<IEnumerable<Humidity>> GetHumidityFromGivenDay(string dateTime, string sensorName);
    }
}