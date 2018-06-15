using System.Threading.Tasks;
using Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure
{

    public class SettingsRepository : ISettingsRepository
    {

        private readonly ApplicationContext _dbContext;
        public SettingsRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void ChangeSetting(Setting setting, string value)
        {
            setting.Value = value;
        }

        public async Task<Setting> GetSetting(string name)
            => await _dbContext.Settings.Where(x => x.Name == name).FirstOrDefaultAsync();    
    }
}