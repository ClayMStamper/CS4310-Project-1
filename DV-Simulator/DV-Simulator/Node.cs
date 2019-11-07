using System.Collections.Generic;
using System.Numerics;

namespace DV_Simulator {
    public class Node {
        
        public int id { get; set; }
        public List<int> links = new List<int>();

        public DistanceVector dv { get; private set; }
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

        public void Flood() {

            Network network = Network.singleton;

            foreach (int link in links) {
                Debug.Log("Node: " + id + " has a link to the following node");
                Debug.Log(network.GetNode(link).ToString());
               // Debug.Log();
                dv.Update(network.GetNode(link).dv);
            }
        }
        
    }
}    