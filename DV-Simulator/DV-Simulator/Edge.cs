namespace DV_Simulator {
    public class Edge {
        
        public Node a { get; set; }
        public Node b {get; set;}
        //used for computing optimal routes
        public int cost {get; set;}

        public Edge(Node a, Node b, int cost) {
            this.a = a;
            this.b = b;
            this.cost = cost;
        }

        public override string ToString() {
            string str;

            str = "a: " + a.id + '\n';
            str += "b: " + b.id + '\n';
            str += "cost: " + cost + "\n\n";
            
            return str;
        }
    }
}