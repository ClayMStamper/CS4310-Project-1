using System;

namespace DV_Simulator {
    public static class Debug {

        public static bool active = true;
        
        public static void Log(string msg) {
            if (!active)
                return;
            
            Console.WriteLine(msg);
            Simulator.singleton.WriteToFile(msg);
        }
    }
}