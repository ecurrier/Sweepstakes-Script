using SweepstakesFinder.Logic;
using SweepstakesScript.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

namespace SweepstakesFinder
{
    public class Program
    {
        static void Main(string[] args)
        {
            CheckForFiles();

            var crawlerLogic = new UrlCrawlerLogic();
            var startingNumber = 0;
            var endingNumber = 0;
            var minutesToExecute = 0;

            Console.WriteLine("Welcome to the SweepstakesFinder!!!\n");

            Console.Write("What Account Number would you like run the SweepstakesFinder logic for? ");
            var accountNumber = Console.ReadLine();

            var mostRecentNumber = RetrieveMostRecentPDFNumber(accountNumber);
            Console.Write($@"
What PDF number would you like to start at?

1. The most recent ({mostRecentNumber}).
2. Set your own number.

Enter: ");

            var startChoice = Console.ReadLine();
            switch(startChoice)
            {
                case "1":
                    startingNumber = int.Parse(mostRecentNumber);
                    break;
                case "2":
                    Console.Write($"\nEnter number to start executing logic for the Account Number {accountNumber}: ");
                    startingNumber = int.Parse(Console.ReadLine());
                    break;
                default:
                    Console.WriteLine("Invalid choice. Exiting...");
                    Console.Read();
                    Environment.Exit(0);
                    break;
            }

            Console.Write($@"
When would you like the program to end?

1. After a certain amount of time.
2. After a certain PDF Number is reached.

Enter: ");

            var endChoice = Console.ReadLine();
            switch (endChoice)
            {
                case "1":
                    Console.Write($"\nEnter time in minutes that the program should execute for: ");
                    minutesToExecute = int.Parse(Console.ReadLine());
                    crawlerLogic.TimedExecute(accountNumber, startingNumber, minutesToExecute);
                    break;
                case "2":
                    Console.Write($"\nEnter PDF Number program should stop executing once it is reached: ");
                    endingNumber = int.Parse(Console.ReadLine());
                    crawlerLogic.LimitExecute(accountNumber, startingNumber, endingNumber);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Press any key to exit...");
                    Console.Read();
                    break;
            }

            Environment.Exit(0);
        }

        private static string RetrieveMostRecentPDFNumber(string accountNumber)
        {
            var accountDictionary = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(File.ReadAllText(Constants.UrlCrawlerTrackerFile));
            if (accountDictionary == null)
            {
                accountDictionary = new Dictionary<string, string>()
                {
                    { accountNumber, "0" }
                };
                File.WriteAllText(Constants.UrlCrawlerTrackerFile, new JavaScriptSerializer().Serialize(accountDictionary));
            }

            if (!accountDictionary.ContainsKey(accountNumber))
            {
                AddToDictionary(accountNumber);
                accountDictionary = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(File.ReadAllText(Constants.UrlCrawlerTrackerFile));
            }

            return accountDictionary[accountNumber];
        }

        private static void AddToDictionary(string accountNumber)
        {
            var accountDictionary = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(File.ReadAllText(Constants.UrlCrawlerTrackerFile));
            accountDictionary.Add(accountNumber, "0");
            File.WriteAllText(Constants.UrlCrawlerTrackerFile, new JavaScriptSerializer().Serialize(accountDictionary));
        }

        private static void CheckForFiles()
        {
            if (!File.Exists(Constants.UrlCrawlerTrackerFile))
            {
                var newFile = File.Create(Constants.UrlCrawlerTrackerFile);
                newFile.Close();
            }

            if (!File.Exists(Constants.UrlsFoundFile))
            {
                var newFile = File.Create(Constants.UrlsFoundFile);
                newFile.Close();
            }
        }
    }
}
