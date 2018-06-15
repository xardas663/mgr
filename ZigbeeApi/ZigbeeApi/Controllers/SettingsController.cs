using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ZigbeeApi.Controllers
{

    public class SettingsController : Controller
    {
        private readonly ISettingsService _settingsService;
        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [Route("api/[controller]")]
        [HttpPost()]
        public async Task<IActionResult> DeleteRoom(string name, string value)
        {
            try
            {
                await _settingsService.ChangeSetting(name, value);
                return Ok();
            }
            catch (System.Exception e)
            {
                return NotFound(e.Message);
            }
         
        }
    }
}