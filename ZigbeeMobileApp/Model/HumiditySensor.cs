using System.Collections.Generic;

namespace Core
{

    public class HumiditySensor : IEntity
    {        
        public string Name { get; set; }
        public string DomoticzId { get; set; }
        public string Desription { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public List<Humidity> Humidity { get; set; }
        public int Id { get; set; }
    }
}