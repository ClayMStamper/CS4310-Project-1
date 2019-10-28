﻿namespace DV_Simulator {
    public class Route {
        public Node destination { get; private set; }
        public Node next { get; private set; }
        public int totalCost { get; private set; }
    }
}