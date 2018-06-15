using System.Threading.Tasks;
using ZigbeeMobileApp.Repository;

namespace ZigbeeMobileApp.Services
{

    public class SettingsService
    {
        public async Task ChangeSetting(string name, string value)
        {
            var repository = new SettingsRepository();
            await repository.ChangeSetting(name, value);
        }
    }
}