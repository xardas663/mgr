using System;

namespace Core
{
    public class Temperature : IEntity
    {
        public int Id { get; set; }        

        public double Value { get; set; }

        public DateTime Date { get; set; }

        public TemperatureSensor TemperatureSensor { get; set; }

        public int TemperatureSensorId { get; set; }
    }
}