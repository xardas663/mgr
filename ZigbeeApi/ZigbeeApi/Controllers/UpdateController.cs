using Core.Json;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ZigbeeApi.Controllers
{
    [Route("api/[controller]")]
    public class UpdateController : Controller
    {
        private readonly IUpdateService _updateService;

        public UpdateController(IUpdateService updateService)
        {
            _updateService = updateService;
        }
        [HttpPost]
        public async Task<IActionResult> Results(string json)
        {
            if (json == null) return NotFound();
            try
            {
                var result = JsonConvert.DeserializeObject<RootObject>(json);
                await _updateService.AddRecievedValues(result);
            }
            catch (System.Exception e)
            {
                return NotFound(e.Message);
            }

            return Ok();
        }
      
    }
}




   