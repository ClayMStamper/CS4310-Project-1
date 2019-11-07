using System.Collections.Generic;
using System.Numerics;

namespace DV_Simulator {
    public class Node {
        
        public int id { get; set; }
        public List<int> links = new List<int>();

        public DistanceVector dv;
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

        public void SetupDistanceVector() {
            
            Network network = Network.singleton;
            int dvWidth = network.nodes.Count;
            dv =  new DistanceVector(this, new int[dvWidth, dvWidth]);

            foreach (int link in links) {
                dv.table[link, link] = network.GetCost(id, link);
            }
            
        }

        public void PrintDistanceVector() {
            
            int nodeCount = Network.singleton.nodes.Count;
            Network network = Network.singleton;
            string d = "\t"; //delimiter

            string dvString = "Node: " + id + "\n";
            
            for (int x = -1; x < nodeCount; x++) {

                if (x >= 0)
                    dvString += x + d; //label Y-axis
                else
                    dvString += d;
                
                for (int y = 0; y < nodeCount; y++) {
                    if (x < 0)
                        dvString += y + d; // label X-axis
                    else if (dv.table[x, y] != 0)
                        dvString += dv.table[x, y] + d;
                    else
                        dvString += "-" + d;
                    
                }

                dvString += "\n";
            }
            Debug.Log(dvString);
        }
        
        public void Flood() {

            int nodeCount = Network.singleton.nodes.Count;
            List<Edge> routes = Network.singleton.routes;
            Network network = Network.singleton;

            foreach (int link in links) {
                dv.table[id, link] = network.GetCost(id, link);
            }
        }
        
    }
}    