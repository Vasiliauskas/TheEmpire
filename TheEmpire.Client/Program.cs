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
            var client = new Client(ConfigurationManager.AppSettings["connection"]);
            client.Start();
            Console.ReadLine();
        }

        // testing purposes
        static void Test()
        {
            var dto = new DTO.EnMapData();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Maps\\classic.map.txt");
            dto.Rows = File.ReadAllLines(path);

            Console.WriteLine("Done");

            var map = new Map(dto,new List<EnPoint>(), new EnPoint());
            //var client = new TacManClient(ConfigurationManager.AppSettings["connection"]);
        }
    }
}
