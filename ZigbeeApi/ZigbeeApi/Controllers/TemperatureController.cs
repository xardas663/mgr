using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ZigbeeApi.Controllers
{

  
    public class TemperatureController : Controller
    {
        private readonly ITemperatureService _temperatureService;
        public TemperatureController(ITemperatureService temperatureService)
        {
            _temperatureService = temperatureService;
        }
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetTemperature(int number = 10, string date = "all", string sensorName = "all")
        {
            try
            {
                var temperature = await _temperatureService.GetTemperature(number, date, sensorName);
                var temperaturesJson = JsonConvert.SerializeObject(temperature);
                return Ok(temperaturesJson);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        
        }
    }
}