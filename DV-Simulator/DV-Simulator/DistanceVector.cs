using System;
using System.Collections.Generic;
using System.Linq;

namespace DV_Simulator {
    public class DistanceVector {
        
        public Node source;
        public int[,] table;
        public bool[,] converged;
        public int[] shortestPath;

        private Network network;
        private int numberConverged;
        
        public DistanceVector(Node source, int[,] table) {
            
            this.source = source;
            this.table = table;
            int length = (int) Math.Sqrt(table.Length);
            shortestPath  = new int[length];
            converged = new bool[length, length];
            network = Network.singleton;
            
            foreach (int link in source.links) {
                int cost = network.GetCost(source.id, link);
                table[link, link] = shortestPath[link] = cost;
            }
            
        }

        public void Update(DistanceVector dvPacket) {

            /*int via = dvPacket.source.id;
            foreach (int link in dvPacket.source.links) {
                int to = link;
                table[via, to] = dvPacket.shortestPath[to] + network.GetCost(source.id, link);
            }*/

            int length = network.nodes.Count;
            int via = dvPacket.source.id;
            
            for (int to = 0; to < length; to++) {
                int toCost = dvPacket.shortestPath[to];
                if (toCost != 0) {
                    int newCost = dvPacket.shortestPath[to] + network.GetCost(source.id, dvPacket.source.id);
                    //add to shortestPath
                    if (table[via, to] == newCost) {
                        numberConverged++;
                        converged[via, to] = true;
                    } else {
                        numberConverged--;
                        table[via, to] = newCost;
                    }
                }

                if (AllAreConverged())
                    network.converged = true;
            }
            
        }


        private bool AllAreConverged() {
            int length = network.nodes.Count;
            for (int i = 0; i < length; i++) {
                for (int j = 0; j < length; j++) {
                    if (!converged[i, j])
                        return false;
                }
            }

            network.converged = true;
            return true;
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