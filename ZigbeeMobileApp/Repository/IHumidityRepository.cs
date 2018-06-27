using System.Collections.Generic;
using System.Threading.Tasks;

using ZigbeeMobileApp.Model;

namespace ZigbeeMobileApp.Repository
{

    public interface IHumidityRepository
    {
        Task<IEnumerable<Humidity>> GetHumidity(int number, string date, string sensorName);
        Task<IEnumerable<HumidityAvgDaily>> GetHumidityAvgDaily(string sensorName);

        Task<IEnumerable<HumidityAvgMonthly>> GetHumidityAvgMonthly(string sensorName);

        Task<IEnumerable<HumidityAvgYearly>> GetHumidityAvgYearly(string sensorName);
    }
}