using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Utils.Settings;

namespace Utils
{
    public class Email
    {
        public static IConfigurationRoot Configuration { get; set; }
        static Email()
        {
            Configuration = ConfigurationHelper.Init();
           

        }
        public static string GetTemplate(string templateUrl)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(templateUrl))
            {
                body = reader.ReadToEnd();
            }
            return body;
        }
        public static void Send(string recepientEmail, string subject, string body, List<string> bcc = null)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
               
                mailMessage.From = new MailAddress(Configuration["Email:EmailDisplay"]);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(recepientEmail));
                if (bcc != null)
                {
                    foreach (var b in bcc)
                    {
                        mailMessage.Bcc.Add(b);
                    }
                }
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Configuration["Email:Host"];
                smtp.EnableSsl = true;
                System.Net.NetworkCredential networkCred = new System.Net.NetworkCredential();
                networkCred.UserName = Configuration["Email:UserName"];
                networkCred.Password = Configuration["Email:Password"];
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkCred;
                smtp.Port = Configuration["Email:MailPort"].ToInt32Return0();
                smtp.Send(mailMessage);
            }
        }

        //public static void SendMail(string receiver, string subject, string body, List<string> bcc = null)
        //{

        //}
    }
}
