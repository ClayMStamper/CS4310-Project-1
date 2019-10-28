using System.Collections.Generic;
using System.Numerics;

namespace DV_Simulator {
    public class Node {
        
        public int id { get; set; }
        public List<int> links = new List<int>();

        public Dictionary<int, Dictionary<int, int>> distanceVector = new Dictionary<int, Dictionary<int, int>>();

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
        }
        
    }
}    