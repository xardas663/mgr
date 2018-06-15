using System.Collections.Generic;
using System.Threading.Tasks;

using ZigbeeMobileApp.Model;

namespace ZigbeeMobileApp.Repository
{

    public interface IHumidityRepository
    {
        Task<IEnumerable<Humidity>> GetHumidity(int number, string date, string sensorName);       
    }
}