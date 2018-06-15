using Core;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ZigbeeApi.Controllers
{

    
    public class RoomsController : Controller
    {
        private readonly IRoomsService _roomsService;
        private readonly IHumiditySensorsService _humiditySensorsService;
        private readonly ITemperatureSensorsService _temperatureSensorsService;

        public RoomsController(IRoomsService roomsService, IHumiditySensorsService humiditySensorsService, ITemperatureSensorsService temperatureSensorsService)
        {
            _roomsService = roomsService;
            _humiditySensorsService = humiditySensorsService;
            _temperatureSensorsService = temperatureSensorsService;
        }
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetAllRooms()
        {
            try
            {
                var rooms = await _roomsService.GetAllRooms();
                return Ok(rooms);
            }
            catch (System.Exception e)
            {
                return NotFound(e.Message);
            }
           
        }

        [Route("api/[controller]")]
        [HttpPost()]
        public async Task<IActionResult> AddRoom(string room)
        {
            try
            {
                var roomObject = JsonConvert.DeserializeObject<Room>(room);
                await _roomsService.AddRoom(roomObject);
                return Ok();
            }
            catch (System.Exception e)
            {
                return NotFound(e.Message);
            }
     
        }

        [Route("api/[controller]")]
        [HttpPut()]
        public async Task<IActionResult> EditRoom(string room)
        {
            try
            {
                var roomObject = JsonConvert.DeserializeObject<Room>(room);
                await _roomsService.EditRoom(roomObject);
                return Ok();
            }
            catch (System.Exception e)
            {
                return NotFound(e.Message);
            }
        
        }

        [Route("api/[controller]")]
        [HttpDelete()]
        public async Task<IActionResult> DeleteRoom(string roomName)
        {
            try
            {
                await _roomsService.DeleteRoom(roomName);
                return Ok();
            }
            catch (System.Exception e)
            {
                return NotFound(e.Message);
            }            
        }   

    }
}