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

        public void Execute(string accountCode, int hourLimit)
        {
            // Create start time
            var startTime = DateTime.UtcNow;
            var minuteLimit = hourLimit * 60;

            // Initialize dictionary
            accountDictionary = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(File.ReadAllText("../../Logic/UrlCrawlerTracker.txt"));
            var count = int.Parse(accountDictionary[accountCode]);

            while ((DateTime.UtcNow - startTime).TotalMinutes < minuteLimit)
            {
                try
                {
                    var url = $"http://rules.rewardpromo.com/rules/{accountCode}-{count}";

                    var request = WebRequest.Create(url);
                    request.Method = "GET";
                    var response = (HttpWebResponse)request.GetResponse();

                    if((int)response.StatusCode == 200)
                    {
                        using (StreamWriter sw = File.AppendText("../../Logic/UrlsFound.txt"))
                        {
                            sw.WriteLine($"{url} - {DateTime.Now.ToString()}");
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.Write($"Exception occured: {ex.Message}\n");
                }

                UpdateCounter(ref count, accountCode);
                RandomSleep();
            }
        }

        private void UpdateCounter(ref int count, string accountCode)
        {
            count++;
            accountDictionary[accountCode] = count.ToString();
            File.WriteAllText("../../Logic/UrlCrawlerTracker.txt", new JavaScriptSerializer().Serialize(accountDictionary));
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
