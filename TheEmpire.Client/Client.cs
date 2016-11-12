using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEmpire.Client
{
    class Client
    {
        private readonly string _serverUrl;
        public Client(string serverUrl)
        {
            _serverUrl = serverUrl;
        }
    }
}
