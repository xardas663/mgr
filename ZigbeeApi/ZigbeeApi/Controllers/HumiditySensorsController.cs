using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ZigbeeApi.Controllers
{

    public class HumiditySensorsController : Controller
    {
        private readonly IHumiditySensorsService _humiditySensorsService;
        public HumiditySensorsController(IHumiditySensorsService humiditySensorsService)
        {
            _humiditySensorsService = humiditySensorsService;
        }
        [Route("api/[controller]")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var humiditySensors = await _humiditySensorsService.GetAll();
                var humidityJson = JsonConvert.SerializeObject(humiditySensors);
                return Ok(humidityJson);
            }
            catch (System.Exception e)
            {
                return NotFound(e.Message);
            }       
        }    
    }
}