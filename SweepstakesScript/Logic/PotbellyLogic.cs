using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesScript.Logic
{
    public class PotbellyLogic
    {
        public PotbellyLogic()
        {

        }

        public void Execute()
        {
            SendEmail("smtp.gmail.com", 587, "ecurri3@gmail.com", "potbellybirthday@yaengage.com", "forget1900");
            SendEmail("smtp.mail.yahoo.com", 587, "emc2207@yahoo.com", "potbellybirthday@yaengage.com", "hrmbpojpoqdgftxf");
        }

        private static void SendEmail(string host, int port, string fromEmail, string toEmail, string password)
        {
            var fromAddress = new MailAddress(fromEmail, "Evan Currier");
            var toAddress = new MailAddress(toEmail);

            var fromPassword = password;
            var subject = "";
            var body = @"Evan Currier
1515 N Fremont St Apt 604
Chicago IL, 60642
618-520-6676
07/22/1993
ecurri3@gmail.com";

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
    }
}
