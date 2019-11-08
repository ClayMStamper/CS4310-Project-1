using System;
using System.Collections.Generic;
using System.IO;

namespace DV_Simulator {
    public class Simulator {

        public static Simulator singleton { get; private set; }

        private StreamWriter writer;
        private string outputPath = "output.txt";
        
        //setup io, read file in, construct network
        public void Run(string roundsRaw, string inputPath) {

            int rounds = int.Parse(roundsRaw);
            
            singleton = this;
            
            InitializeWriter();
            
            Debug.active = true;
            StreamReader reader = new StreamReader(inputPath);
            string topo = reader.ReadToEnd();
            string[] edges = topo.Split('\n');
            
            Network network = new Network(ParseTopology(edges));
            
        }

        private void InitializeWriter() {
            if (File.Exists(outputPath))
                File.WriteAllText(outputPath, "");
            else
                File.Create(outputPath);
            
            writer = new StreamWriter(outputPath);
            writer.AutoFlush = true;
            writer.WriteAsync("Clayton Stamper\nCS 4310 Project 1\n\n");
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

        public void WriteToFile(string msg) {
            writer.WriteLine(msg);
        }
        
    }
}