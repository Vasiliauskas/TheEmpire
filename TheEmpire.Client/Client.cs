using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TheEmpire.Client.DTO;
using TheEmpire.Client.Ghosts;
using TheEmpire.Client.Services;

namespace TheEmpire.Client
{
    class Client
    {
        protected readonly string _serverUrl;
        protected bool _isGameComplete;
        protected readonly GameService _service;

        public Client(string serverUrl)
        {
            _service = new GameService(serverUrl);
        }

        public void Start()
        {
            CreatePlayer();
            while (true)
            {
                var nextTurn = WaitNextTurn();
                if (nextTurn.GameFinished)
                    return;

                if (nextTurn.TurnComplete)
                {
                    if (!TakeTurn()) // failover if one failed, do second
                        TakeTurn();
                }

                Thread.Sleep(50);
            }
        }

        private WaitNextTurnResp WaitNextTurn()
        {
            return _service.WaitNextTurn(_lastTurn);
        }

        private void GetRefTurn()
        {
            throw new NotImplementedException();
        }

        private void CreatePlayer()
        {
            _service.CreatePlayer();
        }

        public virtual void IsGameCompleted()
        {
        }

        protected int _lastTurn = 0;

        protected bool TakeTurn()
        {
            var view = _service.GetPlayerView();
            _lastTurn = view.Turn;
            try
            {
                IEnumerable<Position> req = null;
                if (view.Mode == "TacMan")
                    req = new TacManClient().PerformMove(view);
                else
                    req = new GhostClient().PerformMove(view);

                var responseMove = _service.PerformMove(req);
                // pratesti algo
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        //protected abstract PerformMoveRequest PerformMove(GetPlayerViewResp view);
    }
}
