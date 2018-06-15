using Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IHumidityRepository 
    {
        void AddHumidity(Humidity humidity);
        Task<int> GetHumidityNumber();
        Task<IEnumerable<Humidity>> GetHumidityFromGivenDay(string dateTime, string sensorName);
        Task<IEnumerable<Humidity>> GetAll(int number, string date, string sensorName);
    }
}