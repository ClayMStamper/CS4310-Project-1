using System;

namespace DV_Simulator {
    internal class Program {
        public static void Main(string[] args) {
            Simulator simulator = new Simulator();
            simulator.Run(args[0], "../../../../" + args[1]);
        }
    }

}