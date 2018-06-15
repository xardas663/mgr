using System.Collections.Generic;
using ZigbeeMobileApp.Model;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ZigbeeMobileApp.Repository
{
    public class RoomsRepository : IRoomsRepository
    {
        public async Task AddRoom(Room room)
        {
            var client = new HttpClient();           
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            var roomJson = JsonConvert.SerializeObject(room);
            var values = new Dictionary<string, string>
            {
               { "room", roomJson }
            };
            
            var content = new FormUrlEncodedContent(values);
            var post = await client.PostAsync("http://zigbeeapi.azurewebsites.net/api/rooms", content);
            var response = await post.Content.ReadAsStringAsync();
        }

        public async Task EditRoom(Room room)
        {
            var client = new HttpClient();
            var roomJson = JsonConvert.SerializeObject(room);
            var values = new Dictionary<string, string>
            {
               { "room", roomJson }
            };

            var content = new FormUrlEncodedContent(values);
            var post = await client.PutAsync("http://zigbeeapi.azurewebsites.net/api/rooms", content);
            var response = await post.Content.ReadAsStringAsync();
        }

        public async Task DeleteRoom(string roomName)
        {
            var client = new HttpClient();         
            var post = await client.DeleteAsync($"http://zigbeeapi.azurewebsites.net/api/rooms?roomName={roomName}");
            var response = await post.Content.ReadAsStringAsync();
        }

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"http://zigbeeapi.azurewebsites.net/api/rooms");
            if (response.IsSuccessStatusCode)
            {
                var roomsJson = await response.Content.ReadAsStringAsync(); try
                {
                    var rooms = JsonConvert.DeserializeObject<IEnumerable<Room>>(roomsJson);
                    return rooms;
                }
                catch (System.Exception e)
                {

                    throw e;
                }
                
            }
            else
            {
                return null;
            }
        }
    }
}