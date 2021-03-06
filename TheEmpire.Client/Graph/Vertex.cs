﻿
using System;
using System.Collections.Generic;

namespace TheEmpire.Client.Graph
{
    public partial class Vertex
    {
        public Vertex(int x, int y, bool hasCookie, bool hasGhost, bool hasTacman)
        {
            X = x;
            Y = y;
            HasGhost = hasGhost;
            HasTacman = hasTacman;
            HasCookie = hasCookie;
            Edges = new List<Edge>();
            
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public bool HasGhost { get; set; }
        public bool HasTacman { get; set; }
        public bool HasCookie { get; set; }

        public int Distance { get; set; }
        public Vertex PreviousShortestPathNode { get; set; }

        public List<Edge> Edges { get; private set; }

        public char GetVisualizingChnar()
        {
            if (HasTacman)
                return 'T';

            if (HasGhost)
                return 'G';

            if (HasCookie)
                return '.';

            return ' ';
        }
    }
}
