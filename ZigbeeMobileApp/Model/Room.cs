using System.Collections.Generic;

namespace Core
{

    public class Room : IEntity
    {
        public int Id { get; set; }  

        public string Name { get; set; }

        public string Description { get; set; }

        public float ExpectedTemperature { get; set; }

        public float ExpectedHumidity { get; set; }
        
        public List<TemperatureSensor> TemperatureSensors { get; set; }

        public List<HumiditySensor> HumiditySensors { get; set; }
    }
}
