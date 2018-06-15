using System.Collections.Generic;
using System.Threading.Tasks;
using ZigbeeMobileApp.Model;

namespace ZigbeeMobileApp.Repository
{

    public interface ITemeratureSensorsRepository
    {
        Task<IEnumerable<TemperatureSensor>> GetAll();    
    }
}