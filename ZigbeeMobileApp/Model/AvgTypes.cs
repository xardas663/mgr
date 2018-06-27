using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ZigbeeMobileApp.Model
{
    class AvgTypes
    {
    }

    public class HumidityAvgDaily
    {
        public int HumiditySensorId { get; set; }
        public int AvgHumidity { get; set; }

        public int Day { get; set; }

        public int Month { get; set; }
    }


    public class HumidityAvgMonthly
    {
        public int HumiditySensorId { get; set; }
        public int AvgHumidity { get; set; }

        public int Month { get; set; }
    }


    public class HumidityAvgYearly
    {
        public int HumiditySensorId { get; set; }
        public int AvgHumidity { get; set; }

        public int Year { get; set; }
    }

    public class TemperatureAvgDaily
    {
        public int TemperatureSensorId { get; set; }
        public double AvgTemperature { get; set; }

        public int Day { get; set; }

        public int Month { get; set; }
    }


    public class TemperatureAvgMonthly
    {
        public int TemperatureSensorId { get; set; }
        public double AvgTemperature { get; set; }

        public int Month { get; set; }
    }


    public class TemperatureAvgYearly
    {
        public int TemperatureSensorId { get; set; }
        public double AvgTemperature { get; set; }

        public int Year { get; set; }
    }
}