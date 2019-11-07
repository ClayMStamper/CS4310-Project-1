using System.Collections.Generic;

namespace DV_Simulator {
    public class DVPacket {
        
        public Node source;
        public int[,] distanceVector;

        public DVPacket(Node source, int[,] distanceVector) {
            this.source = source;
            this.distanceVector = distanceVector;
        }

    }
}