using System.Collections.Generic;
using System.Threading.Tasks;
using Core;

namespace ZigbeeMobileApp.Repository
{

    public interface IHumiditySensorsRepository
    {
        Task<IEnumerable<HumiditySensor>> GetAll();
    }
}