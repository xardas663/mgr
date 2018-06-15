using System.Collections.Generic;

namespace ZigbeeMobileApp
{

    public class ListViewRoomsRow
    {        
        public string RoomName { get; set; }
        public string RoomId { get; set; }

        public string Description { get; set; }
        public string TemperatureSensorId { get; set; }
        public string HumiditySensorId { get; set; }        
        public string Humidity { get; set; }
        public string Temperature { get; set; }
        public string ExpectedHumidity { get; set; }
        public string ExpectedTemperature { get; set; }
        public string MaxTemperature { get; set; }
        public string MinTemperature { get; set; }
        public string MaxHumidity { get; set; }
        public string MinHumidity { get; set; }

        public IEnumerable<string> TemperatureSensors { get; set; }
        public IEnumerable<string> HumiditySensors { get; set; }
    }
}