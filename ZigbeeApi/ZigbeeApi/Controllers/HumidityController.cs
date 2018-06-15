using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ZigbeeApi.Controllers
{


    public class HumidityController : Controller
    {
        private readonly IHumidityService _humidityService;
        public HumidityController(IHumidityService humidityService)
        {
            _humidityService = humidityService;
        }
        [Route("api/[controller]")]
        [HttpGet]
        public async Task<IActionResult> GetHumidity(int number = 10, string date="all", string sensorName="all")
        {
            try
            {
                var humidity = await _humidityService.GetHumidity(number, date, sensorName);
                var humidityJson = JsonConvert.SerializeObject(humidity);
                return Ok(humidityJson);
            }
            catch (System.Exception e)
            {
                return NotFound(e.Message);
            }    
        }           
    }
}