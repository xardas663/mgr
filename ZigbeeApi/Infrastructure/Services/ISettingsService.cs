using System.Threading.Tasks;

namespace Infrastructure.Services
{

    public interface ISettingsService
    {
        Task ChangeSetting(string name, string value);
        Task<string> GetSettingValue(string name);
    }
}