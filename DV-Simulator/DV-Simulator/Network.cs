using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DV_Simulator {
    public class Network {
        
        public List<Edge> routes { get; private set; }
        public List<Node> nodes { get; private set; }

        public Network(List<Edge> routes) {
            
            this.routes = routes;
            
            nodes = InitializeNodes();
            SetAllLinks();
            UpdateRoutes();
            
            Debug.Log( ToString());
            
        }

        public override string ToString() {

            string str = "\nNetwork: \n";

            str += "\n==============\n";
            str += "Printing Nodes: \n";
            foreach (Node node in nodes) {
                str += node.ToString() + "\n";
            }
            
            str += "\n==============\n";
            str += "Printing Edges: \n";
            for (int i = 0; i < routes.Count; i++) {
                str +=  "Edge " + i + ":\n" + routes[i];
            }
            str += "==============\n";

            return str;

        }

        private void SetAllLinks() {
            foreach (Edge edge in routes) {
                
                Node node = GetNode(edge.a.id);
                int link = edge.b.id;

                SetLink();
                
                void SetLink() {
                    if (node == null) {
                        nodes.Add(edge.a);
                    } else { 
                        node.AddLink(link);
                        if (GetNode(link) != null)
                            GetNode(link).AddLink(node);
                    }
                }
                
            }
        }

        private List<Node> InitializeNodes() {
            
            List<Node> nodes = new List<Node>();
            
            for (int i = 0; i < CountUniqueNodes(); i++) {
                nodes.Add(new Node(i));
            }

            return nodes;

        }

        private int CountUniqueNodes() {

            int max = 0;
            
            foreach (Edge route in routes) {
                if (route.a.id > max)
                    max = route.a.id;
                if (route.b.id > max)
                    max = route.b.id;
            }

            return max + 1;

        }
        
        public Node GetNode(int id) {
            foreach (Node node in nodes) {
                if (node.id == id)
                    return node;
            }
            return null;
        }

        private void UpdateRoutes() {
            foreach (Edge route in routes) {
                route.a = GetNode(route.a.id);
                route.b = GetNode(route.b.id);
            }
        }
        
    }
    
}