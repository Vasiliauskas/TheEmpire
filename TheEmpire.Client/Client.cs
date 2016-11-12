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
    class Client
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
                    break;

                if(nextTurn.YourTurn && !nextTurn.TurnComplete)
                {
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

        public virtual void TakeTurn()
        {
        }
    }
}
