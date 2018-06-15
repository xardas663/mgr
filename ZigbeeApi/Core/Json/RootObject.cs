using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Json
{
    public class Result
    {
        public double AddjMulti { get; set; }
        public double AddjMulti2 { get; set; }
        public double AddjValue { get; set; }
        public double AddjValue2 { get; set; }
        public int BatteryLevel { get; set; }
        public int CustomImage { get; set; }
        public string Data { get; set; }
        public string Description { get; set; }
        public int Favorite { get; set; }
        public int HardwareID { get; set; }
        public string HardwareName { get; set; }
        public string HardwareType { get; set; }
        public int HardwareTypeVal { get; set; }
        public bool HaveTimeout { get; set; }
        public string ID { get; set; }
        public string LastUpdate { get; set; }
        public string Name { get; set; }
        public string Notifications { get; set; }
        public string PlanID { get; set; }
        public List<int> PlanIDs { get; set; }
        public bool Protected { get; set; }
        public bool ShowNotifications { get; set; }
        public string SignalLevel { get; set; }
        public string SubType { get; set; }
        public double Temp { get; set; }
        public string Timers { get; set; }
        public string Type { get; set; }
        public string TypeImg { get; set; }
        public int Unit { get; set; }
        public int Used { get; set; }
        public string XOffset { get; set; }
        public string YOffset { get; set; }
        public string idx { get; set; }
        public int? Humidity { get; set; }
        public string HumidityStatus { get; set; }
        public string DewPoint { get; set; }
    }

    public class RootObject
    {
        public int ActTime { get; set; }
        public string AstrTwilightEnd { get; set; }
        public string AstrTwilightStart { get; set; }
        public string CivTwilightEnd { get; set; }
        public string CivTwilightStart { get; set; }
        public string DayLength { get; set; }
        public string NautTwilightEnd { get; set; }
        public string NautTwilightStart { get; set; }
        public string ServerTime { get; set; }
        public string SunAtSouth { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
        public List<Result> result { get; set; }
        public string status { get; set; }
        public string title { get; set; }
    }
}
