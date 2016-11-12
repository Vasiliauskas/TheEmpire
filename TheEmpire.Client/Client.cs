using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TheEmpire.Client.DTO;

namespace TheEmpire.Client
{
    class Client
    {
        protected readonly string _serverUrl;
        private GhostClient _ghostClient = new GhostClient();
        private TacManClient _tacManClient = new TacManClient();
        protected ClientService _service;

        public Client(string serverUrl)
        {
            _serverUrl = serverUrl;
            _service = new ClientService(serverUrl);
            _ghostClient = new GhostClient();
            _ghostClient = new GhostClient();
        }

        public void Start()
        {
            GetSessionID();
            CreatePlayer();
            GetRefTurn();
            while (true)
            {
                GetPlayerView(GetPlayerViewReq request);

                var nextTurn = WaitNextTurn();
                if (nextTurn.GameFinished)
                    break;

                if (nextTurn.YourTurn && !nextTurn.TurnComplete)
                {
                    TakeTurn();
                }

                Thread.Sleep(50);
            }
        }

        private WaitNextTurnResp WaitNextTurn(WaitNextTurnReq request)
        {
            throw new NotImplementedException();
        }

        private void GetRefTurn()
        {
            throw new NotImplementedException();
        }

        private void CreatePlayer()
        {
            throw new NotImplementedException();
        }

        private void GetSessionID()
        {
            throw new NotImplementedException();
        }

        public virtual void IsGameCompleted()
        {
        }

        public virtual void TakeTurn()
        {
        }
    }
}
