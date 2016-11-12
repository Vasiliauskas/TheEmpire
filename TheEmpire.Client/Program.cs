using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheEmpire.Client.DTO;
using TheEmpire.Client.MapModels;

namespace TheEmpire.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //var client = new Client(ConfigurationManager.AppSettings["connection"]);
            //client.Start();
            //Console.ReadLine();

            TestGraph();
        }

        // testing purposes
        static void Test()
        {
            var dto = new DTO.EnMapData();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Maps\\classic.map.txt");
            dto.Rows = File.ReadAllLines(path);

            Console.WriteLine("Done");

            var map = new Map(dto, new List<EnPoint>(), new EnPoint());
            //var client = new TacManClient(ConfigurationManager.AppSettings["connection"]);
        }

        private static void TestGraph()
        {
            var dto = new DTO.EnMapData();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Maps\\map5.txt");
            dto.Rows = File.ReadAllLines(path);
            dto.Width = dto.Rows[0].Length;
            dto.Height = dto.Rows.Count();

            //var ghosts = new EnPoint[] { new EnPoint() { Row = 3, Col = 5 } };
            var ghosts = new EnPoint[] { };
            var graph = new Graph.MapGraph(dto, new EnPoint() { Row = 1, Col = 10 }, ghosts);

            // Find some/any cookie
            var cookieLocation = graph.Vertices.FirstOrDefault(c => c.HasCookie == true);
            Console.WriteLine($"Cookie location: {cookieLocation.X}; {cookieLocation.Y}");

            // Set tacman location to arbitrary location
            var tacmanLocation = graph.Vertices.FirstOrDefault(v => v.HasTacman);
            Console.WriteLine($"Tacman location: {tacmanLocation.X}; {tacmanLocation.Y}");

            // Find shortest path between tacman and cookie
            var shortestPath = graph.GetShortestPath(tacmanLocation, cookieLocation);

            // Print graph
            PrintGraph(dto, graph);

            Console.WriteLine("");
            Console.WriteLine("=========================================");
            Console.WriteLine("");

            //Print with shortest path
            PrintGraph(dto, graph, shortestPath);

            Console.ReadKey();
        }

        static void PrintGraph(EnMapData mapDto, Graph.MapGraph graph, IList<Graph.Vertex> pathToDraw)
        {
            for (int i = 0; i < mapDto.Height; i++)
            {
                for (int j = 0; j < mapDto.Width; j++)
                {
                    char symbol;
                    var vertex = graph.Vertices.FirstOrDefault(v => v.X == j && v.Y == i);
                    if (vertex == null)
                    {
                        symbol = mapDto.Rows[i][j];
                    }
                    else
                    {
                        if (pathToDraw.Contains(vertex))
                        {
                            symbol = 'x';
                        }
                        else
                        {
                            symbol = vertex.GetVisualizingChnar();
                        }
                    }
                    Console.Write(symbol);
                }
                Console.WriteLine();
            }
        }

        static void PrintGraph(EnMapData mapDto, Graph.MapGraph graph)
        {
            for (int i = 0; i < mapDto.Height; i++)
            {
                for (int j = 0; j < mapDto.Width; j++)
                {
                    char symbol;
                    var vertex = graph.Vertices.FirstOrDefault(v => v.X == j && v.Y == i);
                    if (vertex == null)
                    {
                        symbol = mapDto.Rows[i][j];
                    }
                    else
                    {
                        symbol = vertex.GetVisualizingChnar();
                    }
                    Console.Write(symbol);
                }
                Console.WriteLine();
            }
        }
    }
}
