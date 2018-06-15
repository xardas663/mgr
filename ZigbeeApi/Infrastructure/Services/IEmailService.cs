using MimeKit;
using System.Threading.Tasks;

namespace Infrastructure.Services
{

    public interface IEmailService
    {
        Task SendEmail(string email, string subject, TextPart body);
        TextPart ConfigureMinHumidityWarningEmailBody(string room, string sensor, string humidity, string minhumidity);
        TextPart ConfigureMaxHumidityWarningEmailBody(string room, string sensor, string humidity, string minhumidity);
        TextPart ConfigureMinTemperatureWarningEmailBody(string room, string sensor, string temperature, string minTemperature);
        TextPart ConfigureMaxTemperatureWarningEmailBody(string room, string sensor, string temperature, string maxTemperature);
    }
}