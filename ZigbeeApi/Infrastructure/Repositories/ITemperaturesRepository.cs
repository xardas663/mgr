using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface ITemperaturesRepository 
    {
        Task<IEnumerable<Temperature>> GetAll(int number, string date, string sensorName);
        void AddTemperature(Temperature temperature);
        Task<IEnumerable<Temperature>> GetTemperaturesFromGivenDay(string dateTime, string sensorName);
    }
}