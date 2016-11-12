using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEmpire.Client.Graph
{
    public partial class Edge
    {
        public Edge(Vertex vertex1, Vertex vertex2, Direction direction)
        {
            Vertex1 = vertex1;
            Vertex2 = vertex2;
            Direction = direction;
        }

        public Direction Direction { get; }

        public Vertex Vertex1 { get; }
        public Vertex Vertex2 { get;  }
    }

    public enum Direction
    {
        TopBottom,
        LeftRight
    }
}
