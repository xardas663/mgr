using System.Threading.Tasks;

namespace Infrastructure.Services
{

    public class SettingsService : ISettingsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SettingsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task ChangeSetting(string name, string value)
        {
            var setting = await _unitOfWork.SettingsRepository.GetSetting(name);
            _unitOfWork.SettingsRepository.ChangeSetting(setting, value);
            await _unitOfWork.CommitChangesAsync();
        }

        public async Task<string> GetSettingValue(string name)
        {
            var setting = await _unitOfWork.SettingsRepository.GetSetting(name);
            return setting.Value;
        }
            
    }
}