using System;
using System.Collections.Generic;
using System.IO;

namespace DV_Simulator {
    public class Simulator {

        public void Run(string path) {

            Debug.active = true;
            StreamReader reader = new StreamReader(path);
            string topo = reader.ReadToEnd();
            string[] edges = topo.Split('\n');
            
            Network network = new Network(ParseTopology(edges));

            
            
        }

        private List<Edge> ParseTopology(string[] rawEdges) {
            
            List<Edge> network = new List<Edge>();
            
            foreach (string edge in rawEdges) {
                string[] splits = edge.Split('\t');
                if (edge == "")
                    break;
                Node a = new Node(int.Parse(splits[0]));
                Node b = new Node(int.Parse(splits[1]));
                int cost = int.Parse(splits[2]);
                network.Add(new Edge(a,b,cost));
            }

            return network;

        }
        
    }
}