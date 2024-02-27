
using System.Net;
using System.Net.Mail;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WebChatServer
{
    public class VerificationEmail
    {
        string _mailBox;

        private SmtpClient GetSmtpClient()
        {
            var config = JsonSerializer.Deserialize<JsonNode>(File.ReadAllText("appconfig.json"));

            string? host = config?["email"]?["gmail"]?["host"]?.ToString();
            string? port = config?["email"]?["gmail"]?["port"]?.ToString();
            string? mailBox = config?["email"]?["gmail"]?["mailbox"]?.ToString();
            string? key = config?["email"]?["gmail"]?["key"]?.ToString();

            if (host == null || port == null || mailBox == null || key == null)
            {
                Console.WriteLine("Error reading configuration file");
                return null;
            }

            _mailBox = mailBox;

            NetworkCredential credential = new(mailBox, key);

            return new()
            {
                Host = host,
                Port = Convert.ToInt32(port),
                Credentials = credential,
                EnableSsl = true
            };
        }

        public void SendVerificationCode(string email, string code)
        {
            using var smtpClient = GetSmtpClient();

            if (smtpClient == null)
            {
                return;
            }

            MailMessage message = new()
            {
                From = new MailAddress(_mailBox!),
                Subject = "Verification Email",
                IsBodyHtml = true,
                Body = $"<h2>Hello, user!</h2><p>Your verification code is: <b>{code}</b></p>"
            };

            message.To.Add(new MailAddress(email));

            try
            {
                smtpClient.Send(message);

                Console.WriteLine($"Verification code sent for {email}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
