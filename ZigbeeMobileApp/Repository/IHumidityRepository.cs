using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using System;

namespace ZigbeeMobileApp.Repository
{

    public interface IHumidityRepository
    {
        Task<IEnumerable<Humidity>> GetHumidity(int number, string date, string sensorName);
        Task<IEnumerable<Humidity>> GetHumidityForGivenDay(DateTime dateTime);
    }
}