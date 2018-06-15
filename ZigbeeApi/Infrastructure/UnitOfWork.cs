using System;
using System.Threading.Tasks;

namespace Infrastructure
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _dbContext;
        private ITemperaturesRepository _temperaturesRepository;
        private IHumidityRepository _humidityRepository;
        private ITemperatureSensorsRepository _temperatureSensorsRepository;
        private IHumiditySensorsRepository _humiditySensorsRepository;
        private IRoomsRepository _roomsRepository;
        private ISettingsRepository _settingsRepository;

        public UnitOfWork(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ITemperatureSensorsRepository TemperatureSensorsRepository
        {
            get
            {
                return _temperatureSensorsRepository = _temperatureSensorsRepository ?? new TemperatureSensorsRepository(_dbContext);
            }
        }

        public IHumiditySensorsRepository HumiditySensorsRepository
        {
            get
            {
                return _humiditySensorsRepository = _humiditySensorsRepository ?? new HumiditySensorsRepository(_dbContext);
            }
        }

        public ITemperaturesRepository TemperaturesRepository
        {
            get
            {
                return _temperaturesRepository = _temperaturesRepository ?? new TemperaturesRepository(_dbContext);
            }
        }

        public IHumidityRepository HumidityRepository
        {
            get
            {
                return _humidityRepository = _humidityRepository ?? new HumidityRepository(_dbContext);
            }
        }

        public IRoomsRepository RoomsRepository
        {
            get
            {
                return _roomsRepository = _roomsRepository ?? new RoomsReposiotry(_dbContext);
            }
        }

        public ISettingsRepository SettingsRepository
        {
            get
            {
                return _settingsRepository = _settingsRepository ?? new SettingsRepository(_dbContext);
            }
        }

        public async Task CommitChangesAsync()
            => await _dbContext.SaveChangesAsync();

        public void CommitChanges()
            => _dbContext.SaveChanges();
        

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                _dbContext.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}