using Core;
using Core.Json;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure
{

    public class UpdateService : IUpdateService
    {
        private readonly ITemperatureSensorsService _temperatureSensorsService;
        private readonly IHumiditySensorsService _humiditySensorsService;
        private readonly ITemperatureService _temperatureService;
        private readonly IHumidityService _humidityService;        
        private readonly IRoomsService _roomsService;
        private readonly IEmailService _emailService;
        private readonly ISettingsService _settingsService;
        public UpdateService(ITemperatureSensorsService temperatureSensorsService, IHumiditySensorsService humiditySensorsService, 
            IRoomsService roomsService, ITemperatureService temperatureService, IHumidityService humidityService, IEmailService emailService, 
            ISettingsService settingsService)
        {
            _temperatureSensorsService = temperatureSensorsService;
            _humiditySensorsService = humiditySensorsService;
            _roomsService = roomsService;
            _temperatureService = temperatureService;
            _humidityService = humidityService;
            _emailService = emailService;
            _settingsService = settingsService;
        }
        public async Task AddRecievedValues(RootObject root)
        {
            var humidity = new List<Humidity>();
            var temp = new List<Temperature>();
            var date = DateTime.Now;
            var email = await _settingsService.GetSettingValue("email");

            foreach (var result in root.result)
            {
                if (result.Type == "Humidity")
                {
                    var sensor = await _humiditySensorsService.GetSensorByDomoticzId(result.idx);
                    if(sensor == null)
                    {
                        var room = await _roomsService.GetRoom("domyslny");
                        if (room == null)
                        {
                            room = CreateNewDefaultRoom();
                            await _roomsService.AddRoom(room);
                        }
                        var newSensor = new HumiditySensor() { Name="Sensor"+DateTime.Now.ToShortTimeString(), Desription="domyslny",DomoticzId = result.idx, Room = room, Humidity = new List<Humidity>() };
                        newSensor.Humidity.Add(new Humidity { Date = date, Value = result.Humidity ?? 0 });
                        await _humiditySensorsService.AddSensor(newSensor);  
                    }                   
                    
                    else
                    {
                        if(result.Humidity > sensor.Room.MaxHumidity)
                        {
                            var body = _emailService.ConfigureMaxHumidityWarningEmailBody(sensor.Room.Name, sensor.Name, result.Humidity.ToString(), sensor.Room.MaxHumidity.ToString());
                            await _emailService.SendEmail(email, "Ostrzeżenie", body);
                        }
                        if (result.Humidity < sensor.Room.MinHumidity)
                        {
                            var body = _emailService.ConfigureMinHumidityWarningEmailBody(sensor.Room.Name, sensor.Name, result.Humidity.ToString(), sensor.Room.MinHumidity.ToString());
                            await _emailService.SendEmail(email, "Ostrzeżenie", body);
                        }                 
                        await _humidityService.AddHumidity(new Humidity { Date = date, Value = result.Humidity ?? 0, HumiditySensorId = sensor.Id });
                    }
                }
                else if (result.Type == "Temp")
                {
                    var sensor = await _temperatureSensorsService.GetSensorsByDomoticzIds(result.idx);
                    if (sensor == null)
                    {
                        var room = await _roomsService.GetRoom("domyslny");
                        if (room == null)
                        {
                            room = CreateNewDefaultRoom();
                            await _roomsService.AddRoom(room);
                        }
                        var newSensor = new TemperatureSensor() { Name = "Sensor" + DateTime.Now.ToShortTimeString(), Desription = "domyslny", DomoticzId = result.idx, Room = room, Temperatures = new List<Temperature>()};
                        newSensor.Temperatures.Add(new Temperature { Date = date, Value = result.Temp });
                        await _temperatureSensorsService.AddSensor(newSensor);                                               
                    }

                    else
                    {
                        if (result.Temp > sensor.Room.MaxTemperature)
                        {
                            var body = _emailService.ConfigureMaxTemperatureWarningEmailBody(sensor.Room.Name, sensor.Name, result.Temp.ToString(), sensor.Room.MaxTemperature.ToString());
                            await _emailService.SendEmail(email, "Ostrzeżenie", body);
                        }
                        if (result.Temp < sensor.Room.MinTemperature)
                        {
                            var body = _emailService.ConfigureMinTemperatureWarningEmailBody(sensor.Room.Name, sensor.Name, result.Temp.ToString(), sensor.Room.MinTemperature.ToString());
                            await _emailService.SendEmail(email, "Ostrzeżenie", body);
                        }
                        await _temperatureService.AddTemperature(new Temperature { Date = date, Value = result.Temp, TemperatureSensorId = sensor.Id });
                    }                    

                }
                else continue;
            }          
        }

        private static Room CreateNewDefaultRoom()
        {
            return new Room()
            {
                Name = "domyslny",
                MaxHumidity = 100,
                MinHumidity = 10,
                MaxTemperature = 40,
                MinTemperature = 10,
                ExpectedHumidity = 50,
                ExpectedTemperature = 21
            };
        }
    }
}