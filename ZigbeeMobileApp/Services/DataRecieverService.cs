using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZigbeeMobileApp.Repository;

namespace ZigbeeMobileApp.Services
{
    public class DataRecieverService
    {
        public async Task<List<ListViewDataRow>> GetDesiredDataFromApiForListView(bool isTemperature, bool isHumidity, int amount, string date, string sensorName)
        {
            if (isTemperature && isHumidity) isHumidity = false;

            if (isTemperature)
            {
                var rowList = CreateNewListForTemperature();
                var repo = new TemperatureRepository();
                var temperatures = await repo.GetTemperatures(amount,date, sensorName);
                foreach (var item in temperatures)
                {
                    rowList.Add(new ListViewDataRow()
                    {
                        RoomName = item.TemperatureSensor.Room.Name.ToString(),
                        SensorName = item.TemperatureSensor.Name.ToString(),
                        Value = item.Value.ToString(),
                        Date = item.Date.ToShortDateString()
                    });
                }
                return rowList;
            }
            else if (isHumidity)
            {
                var rowList = CreateNewListForHumidity();
                var repo = new HumidityRepository();
                var humidity = await repo.GetHumidity(amount, date, sensorName);
                foreach (var item in humidity)
                {
                    rowList.Add(new ListViewDataRow()
                    {
                        RoomName = item.HumiditySensor.Room.Name.ToString(),
                        SensorName = item.HumiditySensor.Name.ToString(),
                        Value = item.Value.ToString(),
                        Date = item.Date.ToShortDateString()
                    });
                }
                return rowList;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<PlotData>> GetTemperatureFromApiForPlot(DateTime datetime)
        {         
                var temperatureList = new List<PlotData>();
                var repo = new TemperatureRepository();
                var temperatures = await repo.GetTemperatureForGivenDay(datetime);
                foreach (var item in temperatures)
                {
                    if (item.TemperatureSensorId == 5) continue;

                    temperatureList.Add(new PlotData()
                    {
                        Value = item.Value,
                        Date = item.Date
                    });
                }
                return temperatureList;                
        }
        public async Task<List<PlotData>> GetHumidityFromApiForPlot(DateTime dateTime)
        {
            var humidityList = new List<PlotData>();
            var repo = new HumidityRepository();
            var humidity = await repo.GetHumidityForGivenDay(dateTime);
            foreach (var item in humidity)
            {
                if (item.HumiditySensorId == 5) continue;
                humidityList.Add(new PlotData()
                {
                    Value = item.Value,
                    Date = item.Date
                });
            }
            return humidityList;
        }

        public async Task<List<ListViewRoomsRow>> GetRooms()
        {
            var roomsList = new List<ListViewRoomsRow>();
            var repo = new RoomsRepository();
            var rooms = await repo.GetAllRooms();
            foreach (var item in rooms)
            {
                roomsList.Add(new ListViewRoomsRow()
                {
                    RoomId = item.Id.ToString(),
                    RoomName = item.Name,
                    Humidity = item.HumiditySensors.Select(x => x.Humidity.Select(y => y.Value.ToString()).FirstOrDefault()).FirstOrDefault(),
                    Temperature = item.TemperatureSensors.Select(x => x.Temperatures.Select(y => y.Value.ToString()).FirstOrDefault()).FirstOrDefault(),
                    ExpectedHumidity = item.ExpectedHumidity.ToString(),
                    ExpectedTemperature = item.ExpectedTemperature.ToString()
                });
            }
            return roomsList;
        }

        public async Task<List<TemperatureSensor>> GetAllTemperatureSensors()
        {
            var repo = new TemperatureSensorsReposiotory();
            var sensors = await repo.GetAll();
            return sensors.ToList();
        }

        public async Task<List<HumiditySensor>> GetAllHumiditySensors()
        {
            var repo = new HumiditySensorsRepository();
            var sensors = await repo.GetAll();
            return sensors.ToList();
        }


        private List<ListViewDataRow> CreateNewListForTemperature()
        {
            var rowList = new List<ListViewDataRow>
                {
                    new ListViewDataRow()
                    {
                        RoomName = "Nazwa pokoju",
                        SensorName = "Nazwa czujnika",
                        Value = "Temperatura [°C]",
                        Date= "Data"
                    }
                };
            return rowList;
        }

        private List<ListViewDataRow> CreateNewListForHumidity()
        {
            var rowList = new List<ListViewDataRow>
                {
                    new ListViewDataRow()
                    {
                        RoomName = "Nazwa pokoju",
                        SensorName = "Nazwa czujnika",
                        Value = "Wilgotność [%]",
                        Date= "Data"
                    }
                };
            return rowList;
        }
    }

  


}