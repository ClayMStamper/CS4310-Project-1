using System.Collections.Generic;

namespace DV_Simulator {
    public class DistanceVector {
        
        public Node source;
        public int[,] table;
        private Network network;

        public DistanceVector(Node source, int[,] table) {
            this.source = source;
            this.table = table;
            network = Network.singleton;
        }

        public void Update(DistanceVector dvPacket) {

            int len = table.Length;
            
            for (int i = 0; i < len; i++) {
                for (int j = 0; j < len; j++) {
                    
                    int mine = table[j, i];
                    int costToThem = table[j, i] != 0 ? table[j, i] : network.GetCost(dvPacket.source.id, source.id);

                    int theirs = dvPacket.table[j, i];
                    if (theirs != 0 && theirs < mine) {
                        table[j, i] = dvPacket.table[j, i];
                    }
                    
                }
            }
            
        }

       private int BestCost(int to) {

           int best = int.MaxValue;
           
           for (int via = 0; via < table.Length; via++) {
               int cost = table[via, to];
               if (cost != 0 && cost < best) //0 means no route
                   best = cost;
           }

           return best;

       }

       public override string ToString() {
           
           int nodeCount = Network.singleton.nodes.Count;
           string d = "\t"; //delimiter

           string dvString = "Node: " + source.id + "\n";
            
           for (int x = -1; x < nodeCount; x++) {

               if (x >= 0)
                   dvString += x + d; //label Y-axis
               else
                   dvString += d;
                
               for (int y = 0; y < nodeCount; y++) {
                   if (x < 0)
                       dvString += y + d; // label X-axis
                   else if (table[x, y] != 0)
                       dvString += table[x, y] + d;
                   else
                       dvString += "-" + d;
                    
               }

               dvString += "\n";
           }

           return dvString;

       }
    }
}