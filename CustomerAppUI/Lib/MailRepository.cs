using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Threading.Tasks;

namespace StudentCourseManagement.Lib
{
    public class MailRepository
    {
        public async Task SendEmail(string toAddress, string subject, string body)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(""); // Your email address
                message.To.Add(new MailAddress(toAddress));
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true; // You can set it to false if sending plain text email

                using (SmtpClient smtpClient = new SmtpClient())
                {
                    smtpClient.Host = "smtp.example.com"; // Your SMTP server address
                    smtpClient.Port = 587; // Your SMTP port (usually 587 for TLS/STARTTLS)
                    smtpClient.EnableSsl = true; // Set it to true if using SSL/TLS
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("", ""); // Your email credentials

                    await smtpClient.SendMailAsync(message);
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}