using System.Collections.Generic;
using System.Numerics;

namespace DV_Simulator {
    public class Node {
        
        public int id { get; set; }
        public List<int> links = new List<int>();

        public int[,] distanceVector;
        public Node(int id) {
            this.id = id;
        }

        public override string ToString() {
            string str = "Node = " + id + " -> ";
            foreach (int link in links) {
                str += link+ ", ";
            }
            return str;
        }

        public bool HasLink(int link) {
            return links.Contains(link);
        }

        public void AddLink(int link) {
            if (!HasLink(link))
                links.Add(link);
        }
        
        public void AddLink(Node link) {
            if (!HasLink(link.id))
                links.Add(link.id);
        }
        
        public void Flood() {

            int nodeCount = Network.singleton.nodes.Count;
            List<Edge> routes = Network.singleton.routes;
            Network network = Network.singleton;

            foreach (int link in links) {
                distanceVector[id, link] = network.GetCost(id, link);
            }
        }

        public void SetupDistanceVector() {
            
            Network network = Network.singleton;
            int dvWidth = network.nodes.Count;
            distanceVector =  new int[dvWidth, dvWidth];
            
        }

        public void PrintDistanceVector() {
            
            int nodeCount = Network.singleton.nodes.Count;
            Network network = Network.singleton;

            string dvString = "Node: " + id + "\n";
            
            for (int x = -1; x < nodeCount; x++) {

                if (x >= 0)
                    dvString += x + "  "; //label Y-axis
                else
                    dvString += "   ";
                
                for (int y = 0; y < nodeCount; y++) {
                    if (x < 0)
                        dvString += y + " "; // label X-axis
                    else if (distanceVector[x, y] != 0)
                        dvString += distanceVector[x, y] + " ";
                    else
                        dvString += "- ";
                    
                }

                dvString += "\n";
            }
            Debug.Log(dvString);
        }
        
    }
}    