using Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure
{

    public class HumidityService : IHumidityService
    {
        private readonly IUnitOfWork _unitOfWork;
        public HumidityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task AddHumidity(Humidity humidity)
        {
            _unitOfWork.HumidityRepository.AddHumidity(humidity);
            await _unitOfWork.CommitChangesAsync();
        }

        public async Task<int> GetHumidityNumber()
            => await _unitOfWork.HumidityRepository.GetHumidityNumber();

        public async Task<IEnumerable<Humidity>> GetHumidityFromGivenDay(string dateTime, string sensorName)
        {            
            var humidity = await _unitOfWork.HumidityRepository.GetHumidityFromGivenDay(dateTime, sensorName);
            return humidity;
        }

        public async Task<IEnumerable<Humidity>> GetHumidity(int number, string date, string sensorName)
       => await _unitOfWork.HumidityRepository.GetAll(number, date, sensorName);
    }
}