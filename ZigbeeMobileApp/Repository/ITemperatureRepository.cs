using System.Collections.Generic;
using System.Threading.Tasks;
using ZigbeeMobileApp.Model;


namespace ZigbeeMobileApp.Repository
{
    public interface ITemperatureRepository
    {
        Task<IEnumerable<Temperature>> GetTemperatures(int number, string date, string sensorName);
    }
}