using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEmpire.Client
{
    class TacManClient : Client
    {
        private readonly ClientService _service;

        public TacManClient(string serverUrl) : base(serverUrl)
        {
            _service = new ClientService(serverUrl);
            var player = _service.CreatePlayer();
            Console.WriteLine();
        }
    }
}
