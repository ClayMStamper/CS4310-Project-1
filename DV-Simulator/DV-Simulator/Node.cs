using System.Collections.Generic;
using System.Numerics;

namespace DV_Simulator {
    public class Node {
        
        public int id { get; set; }
        public List<Node> links;

        public Dictionary<int, Dictionary<int, int>> distanceVector = new Dictionary<int, Dictionary<int, int>>();

        public Node(int id) {
            this.id = id;
        }

        public override string ToString() {
            return "ID = " + id;
        }

        public bool HasLink(Node link) {
            return links.Contains(link);
        }

        public void Flood() {
        }
        
    }
}    