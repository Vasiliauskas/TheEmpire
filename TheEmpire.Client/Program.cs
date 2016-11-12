using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEmpire.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new TacManClient(ConfigurationManager.AppSettings["connection"]);
        }
    }
}
