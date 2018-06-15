using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {        

        public async Task SendEmail(string email, string subject, TextPart body)
        {
            using (var client = new SmtpClient((new ProtocolLogger("smtp.log"))))
            {
                try
                {
                    await ConnectToHost(client);
                    var message = ConfigureMessage(email, subject, body);
                    await client.SendAsync(message);
                }
              
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        private async Task ConnectToHost(SmtpClient client)
        {
            var host = "smtp.gmail.com";
            var port = 465;
            var user = "zigbeepp@gmail.com";
            var password = "wojtek123!";
            client.ServerCertificateValidationCallback = (s, c, ch, e) => true;
            //client.AuthenticationMechanisms.Remove("XOAUTH2");


            await client.ConnectAsync(host, port, SecureSocketOptions.SslOnConnect);
            await client.AuthenticateAsync(user, password);
        }

        private MimeMessage ConfigureMessage(string email, string subject, TextPart text)
        {
            var message = new MimeMessage();
            var fromAddress = "zigbeepp@gmail.com";
            message.From.Add(new MailboxAddress("", fromAddress));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = subject;
            message.Body = text;

            return message;
        }

        public TextPart ConfigureMaxTemperatureWarningEmailBody(string room, string sensor,string temperature, string maxTemperature)
        {           
            var body = new TextPart("plain")
            {
                Text = $"W pomieszczeniu {room} przekroczono dozwoloną wartość temperatury. Maksymalna wartość to {maxTemperature}. Zanotowano {temperature}. Nazwa czujnika: {sensor}. Data: {DateTime.Now}"
            };

            return body;
        }

        public TextPart ConfigureMinTemperatureWarningEmailBody(string room, string sensor, string temperature, string minTemperature)
        {
            var body = new TextPart("plain")
            {
                Text = $"W pomieszczeniu {room} nie osiągnięto minimalnej wartości temperatury. Minimalna wartość to {minTemperature}. Zanotowano {temperature}. Nazwa czujnika: {sensor}. Data: {DateTime.Now}"
            };

            return body;
        }

        public TextPart ConfigureMaxHumidityWarningEmailBody(string room, string sensor, string humidity, string minhumidity)
        {
            var body = new TextPart("plain")
            {
                Text = $"W pomieszczeniu {room}  przekroczono dozwoloną wartość wilgotności. Maksymalna wartość to {minhumidity}. Zanotowano {humidity}. Nazwa czujnika: {sensor}. Data: {DateTime.Now}"
            };

            return body;
        }

        public TextPart ConfigureMinHumidityWarningEmailBody(string room, string sensor, string humidity, string minhumidity)
        {
            var body = new TextPart("plain")
            {
                Text = $"W pomieszczeniu {room} nie osiągnięto minimalnej wartościwilgotności. Minimalna wartość to {minhumidity}. Zanotowano {humidity}. Nazwa czujnika: {sensor}. Data: {DateTime.Now}"
            };

            return body;
        }
    }
}