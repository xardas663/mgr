using Core.Json;
using System.Threading.Tasks;

namespace Infrastructure
{

    public interface IUpdateService
    {
        Task AddRecievedValues(RootObject root);
    }
}