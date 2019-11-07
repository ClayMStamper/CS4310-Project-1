using System.Collections.Generic;

namespace DV_Simulator {
    public class DistanceVector {
        
        public Node source;
        public int[,] table;

        public DistanceVector(Node source, int[,] table) {
            this.source = source;
            this.table = table;
        }

    }
}