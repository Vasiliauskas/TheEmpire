using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheEmpire.Client.DTO;

namespace TheEmpire.Client
{
    class GhostClient : Client
    {
        public GhostClient(string serverUrl) : base(serverUrl)
    {
    }

        protected override PerformMoveRequest PerformMove(GetPlayerViewResp view)
        {
            throw new NotImplementedException();
        }
    }
}
