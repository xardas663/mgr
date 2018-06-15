using System.Threading.Tasks;

namespace Infrastructure
{

    public interface IUnitOfWork
    {
        ITemperaturesRepository TemperaturesRepository { get; }
        IHumidityRepository HumidityRepository { get; }
        ITemperatureSensorsRepository TemperatureSensorsRepository { get; }
        IHumiditySensorsRepository HumiditySensorsRepository { get; }
        IRoomsRepository RoomsRepository { get; }
        ISettingsRepository SettingsRepository {get;}
        Task CommitChangesAsync();
        void CommitChanges();
    }
}