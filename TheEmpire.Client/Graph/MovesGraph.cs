using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheEmpire.Client.DTO;

namespace TheEmpire.Client.Graph
{
    public partial class MovesGraph
    {
        public MovesGraph(EnMapData rawMap, EnPoint tacmanPosition, EnPoint[] ghostPositions)
        {
            Vertices = new List<Vertex>();
            Edges = new List<Edge>();

            // Add all vertices
            for (var i = 0; i < rawMap.Height; i++)
            {
                for (var j = 0; j < rawMap.Width; i++)
                {
                    var x = j;
                    var y = i;
                    var cell = rawMap.Rows[y][x];
                    switch (cell)
                    {
                        case '#':
                            // Wall cells are not connected to anything
                            break;

                        case '.':
                        case ' ':
                            // Empty or cookie cells are connected to 4 adjesent empty or cookie cells
                            var vertex = new Vertex(x, y, cell == '.', false, false);
                            Vertices.Add(vertex);
                            break;

                        default:
                            break;
                    }
                }
            }

            foreach (var vertex in Vertices)
            {
                var vertexAbove = Vertices.FirstOrDefault(v => v.X == vertex.X && v.Y == vertex.Y + 1);
                var vertexBelow = Vertices.FirstOrDefault(v => v.X == vertex.X && v.Y == vertex.Y - 1);

                var vertexRight = Vertices.FirstOrDefault(v => v.Y == vertex.Y && v.X == vertex.X + 1);
                var vertexLeft = Vertices.FirstOrDefault(v => v.Y == vertex.Y && v.X == vertex.X - 1);

                if (vertexAbove != null)
                {
                    var edge = new Edge(vertex, vertexAbove, Direction.TopBottom);
                    vertex.Edges.Add(edge);
                    Edges.Add(edge);
                }
                if(vertexBelow != null)
                {
                    var edge = new Edge(vertex, vertexBelow, Direction.TopBottom);
                    vertex.Edges.Add(edge);
                    Edges.Add(edge);
                }
                if(vertexRight != null)
                {
                    var edge = new Edge(vertex, vertexRight, Direction.LeftRight);
                    vertex.Edges.Add(edge);
                    Edges.Add(edge);
                }
                if(vertexLeft != null)
                {
                    var edge = new Edge(vertex, vertexLeft, Direction.LeftRight);
                    vertex.Edges.Add(edge);
                    Edges.Add(edge);
                }
            }

            // Tacman Position
            var tacmanVertex = Vertices.FirstOrDefault(v => v.X == tacmanPosition.Col && v.Y == tacmanPosition.Row);
            tacmanVertex.HasTacman = true;

            // Ghost Positions
            foreach(var ghostPosition in ghostPositions)
            {
                var ghostVertex = Vertices.FirstOrDefault(v => v.X == ghostPosition.Col && v.Y == ghostPosition.Row);
                ghostVertex.HasGhost = true; 
            }
        }
        
        public List<Vertex> Vertices { get; private set; }
        public List<Edge> Edges { get; private set; }

        
    }
}
