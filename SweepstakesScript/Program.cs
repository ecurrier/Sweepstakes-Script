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
            var amoeLogic = new AMOELogic();

            lithubLogic.Execute();
            reelsweetLogic.Execute();
            amoeLogic.Execute();

            Environment.Exit(0);
        }
    }
}
