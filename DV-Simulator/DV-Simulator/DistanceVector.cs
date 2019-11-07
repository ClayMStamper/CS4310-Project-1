using System;
using System.Collections.Generic;

namespace DV_Simulator {
    public class DistanceVector {
        
        public Node source;
        public int[,] table;
        public int[] shortestPath;
        public int[] newShortestPath;

        private Network network;

        public DistanceVector(Node source, int[,] table) {
            this.source = source;
            this.table = table;
            shortestPath = new int[(int)Math.Sqrt(table.Length)];
            network = Network.singleton;
        }

        public void Update(DistanceVector dvPacket) {

            int len = (int)Math.Sqrt(table.Length);
            
            for (int to = 0; to < len; to++) {
                for (int via = 0; via < len; via++) {
                    int mine = table[via, to];
                    
                    int costToThem = table[via, to] != 0 ? table[via, to] : network.GetCost(dvPacket.source.id, source.id);
                    
                    int theirBest = BestCost(to);
                    int costToCheck = costToThem + theirBest;
                    
                    if (costToCheck != 0 && costToCheck < table[dvPacket.source.id, to] || table[dvPacket.source.id, to] != 0 ) {
                        table[dvPacket.source.id, to] = theirBest;
                    }
                    
                }
            }
            
        }

       private int BestCost(int to) {

           int best = int.MaxValue;
           
           for (int via = 0; via < Math.Sqrt(table.Length); via++) {
               int cost = table[via, to];
               if (cost != 0 && cost < best) //0 means no route
                   best = cost;
           }

           return best == int.MaxValue ? 0 : best;

       }

       public override string ToString() {
           
           int nodeCount = Network.singleton.nodes.Count;
           string d = "\t"; //delimiter

           string dvString = "Node: " + source.id + "\n   via\nto";
            
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