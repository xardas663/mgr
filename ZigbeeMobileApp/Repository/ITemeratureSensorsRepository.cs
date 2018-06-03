using System.Collections.Generic;
using System.Threading.Tasks;
using Core;

namespace ZigbeeMobileApp.Repository
{

    public interface ITemeratureSensorsRepository
    {
        Task<IEnumerable<TemperatureSensor>> GetAll();    
    }
}