using System.Threading.Tasks;
using Core;

namespace Infrastructure
{

    public interface ISettingsRepository
    {
        Task<Setting> GetSetting(string name);
        void ChangeSetting(Setting setting, string value);
    }
}