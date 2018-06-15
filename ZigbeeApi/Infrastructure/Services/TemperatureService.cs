using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure
{

    public class TemperatureService : ITemperatureService
    {
        IUnitOfWork _unitOfWork;
        public TemperatureService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Temperature>> GetTemperature(int number, string date, string sensorName)
            => await _unitOfWork.TemperaturesRepository.GetAll(number,date,sensorName);

        public async Task AddTemperature (Temperature temperature)
        {
            _unitOfWork.TemperaturesRepository.AddTemperature(temperature);
            await _unitOfWork.CommitChangesAsync();
        }

        public async Task<IEnumerable<Temperature>> GetTemperatureFromGivenDay(string dateTime, string sensorName)
        {
            var temperatures = await _unitOfWork.TemperaturesRepository.GetTemperaturesFromGivenDay(dateTime, sensorName);
            return temperatures;
        }
    }
}