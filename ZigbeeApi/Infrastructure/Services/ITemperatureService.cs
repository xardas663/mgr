using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure
{

    public interface ITemperatureService
    {
        Task<IEnumerable<Temperature>> GetTemperature(int number, string date, string sensorName);
        Task AddTemperature(Temperature temperatures);
        Task<IEnumerable<Temperature>> GetTemperatureFromGivenDay(string dateTime, string sensorName);
    }
}