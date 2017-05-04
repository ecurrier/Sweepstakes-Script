using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace SweepstakesScripter.Logic
{
    public class LitHubLogic
    {
        // Each email is encrypted into a string. This is appended to the end of the confirmation url
        private static Dictionary<string, string> EmailEncryptedStrings = new Dictionary<string, string>
        {
            { "emc2207@yahoo.com",  "239b15a652" },
            { "ecurri3@gmail.com", "48704ad5c0"},
            { "loken.erica@gmail.com", "340a8420d5" },
            { "erica.loken@yahoo.com", "d0aadc49cf"},
            { "sarahbeth.ellison@gmail.com", "4d4aae6b6d" },
            { "sarahbella224@aol.com", "58f274852b" }
        };

        private static string uParameter = "5d9b50f912e18fb44e8d7b091";
        private static string idParameter = "d4da95b253";

        public LitHubLogic() {

        }

        public void Execute() {
            // Enter each email in the subscription list
            foreach (var email in EmailEncryptedStrings)
            {
                var url = "http://literaryhub.us9.list-manage.com/subscribe/post?u=" + uParameter + "&id=" + idParameter;
                var data = Encoding.ASCII.GetBytes($"EMAIL={email.Key}&b_5d9b50f912e18fb44e8d7b091_d4da95b253=&subscribe=Enter");

                var request = WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                WebResponse response = request.GetResponse();
                response.Close();
            }

            Thread.Sleep(5000);

            // Mimic the action of clicking the confirmation url sent to each email address
            foreach (var email in EmailEncryptedStrings)
            {
                var url = "https://literaryhub.us9.list-manage.com/subscribe/confirm?u=" + uParameter + "&id=" + idParameter + "&e=" + email.Value;

                var request = WebRequest.Create(url);
                request.Method = "GET";

                try
                {
                    var response = request.GetResponse();
                    Console.WriteLine($"{email.Key} Successfully entered into LitHub Sweepstakes.");
                    response.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
