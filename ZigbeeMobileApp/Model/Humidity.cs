using System;

namespace Core
{

    public class Humidity: IEntity
    {        
        public int Value { get; set; }

        public DateTime Date { get; set; }

        public HumiditySensor HumiditySensor { get; set; }
        public int HumiditySensorId { get; set; }
        public int Id { get; set; }
    }

    
}