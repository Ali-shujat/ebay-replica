using System.Net.Mail;
using System.Text;

namespace eCommerce.Services
{
    public static class SendEmail
    {
        public static string CreateEmail(string email, string tocken)
        {
            string to = email; //To address    
            string from = "industransport@outlook.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = tocken;
            message.Subject = "Sending Email Using Asp.Net & C#";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.live.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential(from, "Cda003ojb464");
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
    }
}
