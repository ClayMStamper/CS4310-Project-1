using System;
using System.Collections.Generic;

namespace DV_Simulator {
    public class Network {
        
        public List<Edge> routes { get; private set; }
        public List<Node> nodes { get; private set; }

        public Network(List<Edge> routes) {
            this.routes = routes;
            foreach (Edge route in routes) {
                UpdateNode(route);
            }
        }

        private void UpdateNode(Edge edge) {

            Node node = edge.a;
            Node link = edge.b;

            if (node == null) {
                Debug.Log(node + " is new, adding to Network!");
                nodes.Add(node);
            } else {
                if (node.HasLink(link))
                    Debug.Log("Error: " + node + " already has link: " + link + " but is trying to add link");
                else 
                    node.links.Add(link);
            }

        }

        public Node GetNode(int id) {
            foreach (Node node in nodes) {
                if (node.id == id)
                    return node;
            }
            return null;
        }
        
    }
}