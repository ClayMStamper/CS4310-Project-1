using System;
using System.Collections.Generic;

namespace DV_Simulator {
    public class DistanceVector {
        
        public Node source;
        public int[,] table;
        public bool[,] converged;
        public int[] shortestPath;
        public int[] newShortestPath;

        private Network network;

        public DistanceVector(Node source, int[,] table) {
            
            this.source = source;
            this.table = table;
            int length = (int) Math.Sqrt(table.Length);
            shortestPath = newShortestPath = new int[length];
            converged = new bool[length, length];
            network = Network.singleton;
            
            foreach (int link in source.links) {
                int cost = network.GetCost(source.id, link);
                table[link, link] = newShortestPath[link] = cost;
            }
            
        }

        public void Update(DistanceVector dvPacket) {

            int via = dvPacket.source.id;
            foreach (int link in dvPacket.source.links) {
                int to = link;
                table[via, to] = dvPacket.BestCost(to);
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
                   if (x >= 0 && (x == source.id || y == source.id))
                       dvString += "x" + d; // mark off to myself and via myself
                   else if (x < 0)
                       dvString += y + d; // label X-axis
                   else if (table[x, y] != 0)
                       dvString += table[x, y] + d;
                   else
                       dvString += "-" + d; //unassigned values left blank

               }

               dvString += "\n";
           }

           return dvString;

       }
    }
}