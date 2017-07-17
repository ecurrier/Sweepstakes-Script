using SweepstakesScript.Logic;
using System;

namespace SweepstakesScript
{
    class Program
    {
        static void Main(string[] args)
        {
            var lithubLogic = new LitHubLogic();
            var reelsweetLogic = new ReelSweetLogic();
            var potbellyLogic = new PotbellyLogic();

            lithubLogic.Execute();
            reelsweetLogic.Execute();
            potbellyLogic.Execute();

            Environment.Exit(0);
        }
    }
}
