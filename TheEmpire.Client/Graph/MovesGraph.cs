using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheEmpire.Client.DTO;

namespace TheEmpire.Client.Graph
{
    public partial class MapGraph
    {
        public MapGraph(EnMapData rawMap, EnPoint tacmanPosition, EnPoint[] ghostPositions)
        {
            Vertices = new List<Vertex>();
            Edges = new List<Edge>();

            // Add all vertices
            for (var j = 0; j < rawMap.Width; j++)
            {
                for (var i = 0; i < rawMap.Height; i++)
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
                if (vertexBelow != null)
                {
                    var edge = new Edge(vertex, vertexBelow, Direction.TopBottom);
                    vertex.Edges.Add(edge);
                    Edges.Add(edge);
                }
                if (vertexRight != null)
                {
                    var edge = new Edge(vertex, vertexRight, Direction.LeftRight);
                    vertex.Edges.Add(edge);
                    Edges.Add(edge);
                }
                if (vertexLeft != null)
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
            foreach (var ghostPosition in ghostPositions)
            {
                var ghostVertex = Vertices.FirstOrDefault(v => v.X == ghostPosition.Col && v.Y == ghostPosition.Row);
                ghostVertex.HasGhost = true;
            }
        }

        public List<Vertex> Vertices { get; private set; }
        public List<Edge> Edges { get; private set; }

        public bool Traversable(Vertex start, Vertex destination)
        {
            foreach (var v in Vertices)
            {
                v.PreviousShortestPathNode = null;
                v.Distance = 0;
            }

            var visited = new HashSet<Vertex>();
            var queue = new Queue<Vertex>();
            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                var t = queue.Dequeue();
                
                if (t == destination)
                    return true;

                var adjacentVertices = t.Edges.Select(s => s.Vertex1).Union(t.Edges.Select(s => s.Vertex2));
                foreach (var adjacentVertex in adjacentVertices)
                {
                    if (!visited.Contains(adjacentVertex))
                    {
                        visited.Add(adjacentVertex);
                        queue.Enqueue(adjacentVertex);
                    }
                }
            }

            return false;
        }

        public List<Vertex> GetShortestPath(Vertex start, Vertex end)
        {
            var q = new HashSet<Vertex>();
            foreach (var vertex in Vertices)
            {
                vertex.Distance = Int32.MaxValue;
                vertex.PreviousShortestPathNode = null;
                q.Add(vertex);
            }

            start.Distance = 0;

            while (q.Count > 1)
            {
                var vertexWithMinDistance = q.OrderBy(o => o.Distance).FirstOrDefault();

                if (vertexWithMinDistance != end)
                {
                    q.Remove(vertexWithMinDistance);

                    var adjacentVertices = vertexWithMinDistance.Edges.Select(e => e.Vertex1).Union(vertexWithMinDistance.Edges.Select(s => s.Vertex2)).Intersect(q);
                    foreach (var adjacentVertex in adjacentVertices)
                    {
                        var altDistance = vertexWithMinDistance.Distance + 1;
                        if (altDistance < adjacentVertex.Distance)
                        {
                            adjacentVertex.Distance = altDistance;
                            adjacentVertex.PreviousShortestPathNode = vertexWithMinDistance;
                        }
                    }
                }
                else
                {
                    // Here we have shortest path, just need to reverse it
                    var stack = new Stack<Vertex>();
                    var current = end;

                    while (current != null)
                    {
                        stack.Push(current);
                        current = current.PreviousShortestPathNode;
                    }
                    return stack.ToList();
                }
            }

            // No path could be found
            return null;
        }

    }
}
