using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace DomoticzDataReciever.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var client = new HttpClient();
            
            //var response = await client.GetAsync("<admin:admin@>http://127.0.0.1:8080/json.htm?type=devices&filter=all&used=true&order=Name");
            var response = await client.GetAsync("http://127.0.0.1:8080/json.htm?username=YWRtaW4=&password=YWRtaW4=&type=devices&filter=temp&used=true&order=Name");
            var odp = await response.Content.ReadAsStringAsync();

            var values = new Dictionary<string, string>
            {
               { "json", odp }               
            };

            var content = new FormUrlEncodedContent(values);
            
            var post = await client.PostAsync("http://zigbeeapi.azurewebsites.net/api/update", content);
            var message = await post.Content.ReadAsStringAsync();
            //var post = await client.PostAsync("http://localhost:50691/api/update", content);
            return Ok(odp);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
