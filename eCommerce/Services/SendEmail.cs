using System.Net;
using System.Net.Mail;
using System.Text;


namespace eCommerce.Services
{
    public static class SendEmail
    {
        public static string CreateEmail(string email, string tocken)
        {

            string from = "the.shujat@hotmail.com"; // Replace with your own email address
            string password = "Obliviate1"; // Replace with your own email password   
            MailMessage message = new MailMessage(from, email);

            string mailbody = tocken;
            message.Subject = "Sending Email Using Asp.Net & C#";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            //SmtpClient client = new SmtpClient("smtp.live.com", 587); //Gmail smtp    
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587); //outlook smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential(from, password);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return client.ClientCertificates[0].ToString();
        }



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
