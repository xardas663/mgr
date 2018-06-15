using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ZigbeeApi.Controllers
{

    public class TemperatureSensorsController : Controller
    {
        private readonly ITemperatureSensorsService _temperatureSensorsService;
        public TemperatureSensorsController(ITemperatureSensorsService temperatureSensorsService)
        {
            _temperatureSensorsService = temperatureSensorsService;
        }

        [Route("api/[controller]")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var temperatureSensors = await _temperatureSensorsService.GetAll();
                var temperatureJson = JsonConvert.SerializeObject(temperatureSensors);
                return Ok(temperatureJson);
            }
            catch (System.Exception e)
            {
                return NotFound(e.Message);
            }
         
        }

    }
}