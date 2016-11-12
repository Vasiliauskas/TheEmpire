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
        protected bool _isGameComplete;
        protected ClientService _service;

        public Client(string serverUrl)
        {
            _serverUrl = serverUrl;
            _service = new ClientService(serverUrl);
        }

        public void Start()
        {
            GetSessionID();
            CreatePlayer();
            GetRefTurn();
            while (true)
            {
                var nextTurn = WaitNextTurn();
                if (nextTurn.GameFinished)
                    return;

                if(nextTurn.YourTurn && !nextTurn.TurnComplete)
                {
                    if (!TakeTurn()) // failover if one failed, do second
                        TakeTurn();
                }

                Thread.Sleep(50);
            }
        }

        private WaitNextTurnResp WaitNextTurn()
        {
            throw new NotImplementedException();

            //_service.WaitNextTurn();
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

        protected bool TakeTurn()
        {
            var view = _service.GetPlayerView(1);
            try
            {
                IEnumerable<Position> req = null;
                if (view.Mode == "TacMan")
                    req = new TacManClient().PerformMove(view);
                //else
                //    new GhostClient();

                var responseMove = _service.PerformMove(req);
                // pratesti
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        //protected abstract PerformMoveRequest PerformMove(GetPlayerViewResp view);
    }
}
