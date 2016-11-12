using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TheEmpire.Client.DTO;
using TheEmpire.Client.Services;

namespace TheEmpire.Client
{
    abstract class Client
    {
        protected readonly string _serverUrl;
        private GhostClient _ghostClient = new GhostClient();
        private TacManClient _tacManClient = new TacManClient();
        protected ClientService _service;
        protected bool _isGameComplete;
        protected readonly GameService _service;

        public Client(string serverUrl)
        {
            _service = new GameService(serverUrl);
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
                    return;

                if (nextTurn.YourTurn && !nextTurn.TurnComplete)
                {
                    if (!TakeTurn()) // failover if one failed, do second
                    TakeTurn();
                }

                Thread.Sleep(50);
            }
        }

        private WaitNextTurnResp WaitNextTurn(WaitNextTurnReq request)
        {
            return _service.WaitNextTurn();
        }

        private void GetRefTurn()
        {
            throw new NotImplementedException();
        }

        private void CreatePlayer()
        {
            _service.CreatePlayer();
        }

        private void GetSessionID()
        {
            throw new NotImplementedException();
        }

        public virtual void IsGameCompleted()
        {
        }

        protected bool TakeTurn()
        {
            var view = _service.GetPlayerView();
            try
            {
                var resp = PerformMove(view);
                var responseMove = _service.PerformMove(resp);
                // pratesti
            }
            catch(Exception ex)
        {
                return false;
            }
            return true;
        }

        protected abstract PerformMoveRequest PerformMove(GetPlayerViewResp view);
    }
}
