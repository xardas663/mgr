using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ZigbeeMobileApp.Repository
{

    public class SettingsRepository
    {
        public async Task ChangeSetting(string name, string value)
        {
            var client = new HttpClient();          
    
            var values = new Dictionary<string, string>
            {
               { "name", name },
               {"value", value }
            };

            var content = new FormUrlEncodedContent(values);
            var post = await client.PostAsync("http://zigbeeapi.azurewebsites.net/api/settings", content);
            var response = await post.Content.ReadAsStringAsync();
        }
    }
}