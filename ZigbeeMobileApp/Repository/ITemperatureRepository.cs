using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using System;

namespace ZigbeeMobileApp.Repository
{
    public interface ITemperatureRepository
    {
        Task<IEnumerable<Temperature>> GetTemperatures(int number, string date, string sensorName);
        Task<IEnumerable<Temperature>> GetTemperatureForGivenDay(DateTime dateTime);
    }
}