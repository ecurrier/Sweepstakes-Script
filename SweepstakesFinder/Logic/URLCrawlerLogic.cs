using SweepstakesFinder.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Web.Script.Serialization;

namespace SweepstakesScript.Logic
{
    public class UrlCrawlerLogic
    {
        private Dictionary<string, string> accountDictionary;

        public void TimedExecute(string accountCode, int startingNumber, int minuteLimit)
        {
            Console.Clear();
            Console.WriteLine($"Running logic for {minuteLimit} minutes...");
            
            accountDictionary = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(File.ReadAllText(Constants.UrlCrawlerTrackerFile));

            var startTime = DateTime.UtcNow;
            while ((DateTime.UtcNow - startTime).TotalMinutes < minuteLimit)
            {
                RequestUrl(accountCode, startingNumber);
                UpdateCounter(ref startingNumber, accountCode);
                RandomSleep();
            }
        }

        public void LimitExecute(string accountCode, int startingNumber, int endingNumber)
        {
            Console.Clear();
            Console.WriteLine($"Running logic until {accountCode}-{endingNumber}...");

            accountDictionary = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(File.ReadAllText(Constants.UrlCrawlerTrackerFile));

            while (startingNumber < endingNumber)
            {
                RequestUrl(accountCode, startingNumber);
                UpdateCounter(ref startingNumber, accountCode);
                RandomSleep();
            }
        }

        private void RequestUrl(string accountCode, int pdfNumber)
        {
            try
            {
                var url = $"http://rules.rewardpromo.com/rules/{accountCode}-{pdfNumber}";

                var request = WebRequest.Create(url);
                request.Method = "GET";
                request.Timeout = 10000;

                var response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine($"A valid request was received from {url}");
                    using (StreamWriter sw = File.AppendText(Constants.UrlsFoundFile))
                    {
                        sw.WriteLine($"{url} - {DateTime.Now.ToString()}");
                    }
                }

            }
            catch (Exception) {}
        }

        private void UpdateCounter(ref int pdfNumber, string accountCode)
        {
            pdfNumber++;
            accountDictionary[accountCode] = pdfNumber.ToString();
            File.WriteAllText(Constants.UrlCrawlerTrackerFile, new JavaScriptSerializer().Serialize(accountDictionary));
        }

        private void RandomSleep()
        {
            var random = new Random();
            var maxValue = 4f;
            var minValue = 2f;

            var range = maxValue - minValue;
            var sample = random.NextDouble();
            var scaled = (sample * range) + minValue;

            var sleepTime = (float)scaled * 1000;

            Thread.Sleep((int)sleepTime);
        }
    }
}
