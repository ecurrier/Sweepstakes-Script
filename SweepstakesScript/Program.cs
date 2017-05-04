using SweepstakesScripter.Logic;
using System;

namespace Nebuchenazar
{
    class Program
    {
        static void Main(string[] args)
        {
            var lithubLogic = new LitHubLogic();
            var reelsweetLogic = new ReelSweetLogic();

            lithubLogic.Execute();
            reelsweetLogic.Execute();

            Environment.Exit(0);
        }
    }
}
