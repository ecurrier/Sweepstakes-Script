using System;
using System.Net;
using System.Net.Mail;

namespace SweepstakesScripter.Logic
{
    public class AMOELogic
    {
        public AMOELogic()
        {

        }

        public void Execute()
        {
            SendEmail("smtp.gmail.com", 587, "ecurri3@gmail.com", "entry@amoeentry.com", "forget1900", "Evan Currier");
            SendEmail("smtp.mail.yahoo.com", 587, "emc2207@yahoo.com", "entry@amoeentry.com", "hrmbpojpoqdgftxf", "Evan Currier");
        }

        private static void SendEmail(string host, int port, string fromEmail, string toEmail, string password, string displayName)
        {
            var fromAddress = new MailAddress(fromEmail, displayName);
            var toAddress = new MailAddress(toEmail);

            var fromPassword = password;
            var subject = FormatDate();
            var body = displayName;

            var smtp = new SmtpClient
            {
                Host = host,
                Port = port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

        private static string FormatDate()
        {
            var today = DateTime.Today;
            string mm = null;
            string dd = null;
            string yy = null;

            var month = today.Month;
            if (month < 10)
            {
                mm = $"0{month}";
            }

            var day = today.Day;
            if (day < 10)
            {
                dd = $"0{day}";
            }

            var year = today.Year.ToString();
            yy = year.Substring(2);

            return $"{mm}/{dd}/{yy}";
        }
    }
}
