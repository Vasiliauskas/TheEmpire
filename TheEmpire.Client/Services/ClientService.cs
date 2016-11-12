using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEmpire.Client
{
    class ClientService
    {
        private readonly string _serviceUrl;
        public ClientService(string host)
        {
            _serviceUrl = Path.Combine(host, "/ClientService.svc/json");
        }

        public bool CreatePlayer()
        {
            throw new NotImplementedException();
        }

        public bool WaitNextTurn()
        {
            throw new NotImplementedException();
        }

        public bool GetPlayerView()
        {
            throw new NotImplementedException();

        }

        public bool PerformMove()
        {
            throw new NotImplementedException();
        }
    }
}
