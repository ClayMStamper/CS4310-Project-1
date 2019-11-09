using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Security.Permissions;

namespace DV_Simulator {
    public class Network {

        public static Network singleton;
        
        public List<Edge> routes { get; private set; }
        public List<Node> nodes { get; private set; }
        
        public bool converged;
        public int roundsTaken;
        public int lastNodeConverged;

        private int rounds;

        //initialize nodes, initialize links, initialize routes, flood routing tables
        public Network(List<Edge> routes, int rounds) {
            
            this.routes = routes;
            this.rounds = rounds;
            singleton = this;
            
            nodes = InitializeNodes();
            SetAllLinks();
            UpdateRoutes();
            SetupDistanceVectors();
            FloodRoutingTables();
            PrintConvergence();
            PrintRoutingTables();
            
            
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
        private void SetupDistanceVectors() {
            
            foreach (Node node in nodes)
                node.SetupDistanceVector();
        }

        private void FloodRoutingTables() {
            Debug.Log("Flooding for " + rounds + " rounds\n");
            for (int i = 0; i < rounds; i++) {
                
                if (converged) {
                    roundsTaken = i;
                    return;
                }
                    
                foreach (Node node in nodes) 
                    node.Flood();
                if (!converged)
                    roundsTaken++;
            }
        }

        private void PrintConvergence() {
            if (converged) {
                Debug.Log("Last node to converge was Node: " + lastNodeConverged);
                Debug.Log("Last node converged after: " + roundsTaken + " rounds.\n");
            } else {
                Debug.Log("Nodes were unable to converge in: " + roundsTaken + " rounds...\n");
            }
        }

        private void PrintRoutingTables() {
            foreach (Node node in nodes)
                Debug.Log(node.dv.ToString());
        }

        public Node GetNode(int id) {
            foreach (Node node in nodes) {
                if (node.id == id)
                    return node;
            }
            Debug.Log("=========\nError: Could not find node with ID:\n " + id);
            return null;
        }

        private void UpdateRoutes() {
            foreach (Edge route in routes) {
                route.a = GetNode(route.a.id);
                route.b = GetNode(route.b.id);
            }
        }

        public int GetCost(int x, int y) {

            foreach (Edge route in routes) {
                if (x == route.a.id && y == route.b.id) {
                    return route.cost;
                }
                if (x == route.b.id && y == route.a.id) {
                    return route.cost;
                }
            }
            Debug.Log("\n\nError: route from " + x + " to " + y + " was not found!!!\n\n");
            return 0;
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
        
    }
}