using System;

namespace Core
{

    public class WeatherOutside : IEntity
    {
        public int Id { get; set; }       

        public float Temperature { get; set; }

        public float Humidity { get; set; }

        public DateTime Date { get; set; }
    }
}