using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TheEmpire.Client
{
    class Client
    {
        private readonly string _serverUrl;
        private bool _isGameComplete;
        public Client(string serverUrl)
        {
            _serverUrl = serverUrl;
        }

        public void Start()
        {
            GetSessionID();
            CreatePlayer();
            GetRefTurn();
            while (!_isGameComplete)
            {
                WaitNextTurn();
                Thread.Sleep(5);
            }
        }

        private void WaitNextTurn()
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
