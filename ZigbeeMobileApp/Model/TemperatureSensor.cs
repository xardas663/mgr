using System.Collections.Generic;

namespace Core
{

    public class TemperatureSensor : IEntity
    {
        public int Id { get; set; }
        public string DomoticzId { get; set; }
        public string Name { get; set; }        
        public string Desription { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public List<Temperature> Temperatures {get; set;}
    }
}