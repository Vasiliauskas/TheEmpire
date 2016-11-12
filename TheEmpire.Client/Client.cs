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
        protected bool _isGameComplete;
        protected readonly GameService _service;

        public Client(string serverUrl)
        {
            _service = new GameService(serverUrl);
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
