using System.Net;
using System.Net.Mail;


namespace eCommerce.Services
{
    public static class SendEmail
    {
        public static void SkickaEmail(string to, string password, string body)
        {
            string from = "the.shujat@hotmail.com"; // Replace with your own email address
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587); // Replace with your SMTP server and port number
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(from, password);
            client.EnableSsl = true;

            MailMessage message = new MailMessage(from, to, "Verification code from EStore!", body);
            message.IsBodyHtml = true;

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: {0}", ex.Message);
            }
        }

    }
}
