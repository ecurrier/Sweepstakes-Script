using SweepstakesScript.Logic;
using System;

namespace SweepstakesFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            var crawlerLogic = new UrlCrawlerLogic();

            crawlerLogic.Execute("863", 3);

            Environment.Exit(0);
        }
    }
}
