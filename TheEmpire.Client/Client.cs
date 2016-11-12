using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TheEmpire.Client.DTO;
using TheEmpire.Client.Services;

<<<<<<< HEAD
namespace TheEmpire.Client
{
    abstract class Client
    {
        protected bool _isGameComplete;
        protected readonly GameService _service;

        public Client(string serverUrl)
        {
            _service = new GameService(serverUrl);
=======
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
>>>>>>> e71f7621b5666e47f8dcca2d5445609684725bc4
        }

        public void Start()
        {
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

        public virtual void IsGameCompleted()
        {
        }

        protected bool TakeTurn()
        {
            var view = _service.GetPlayerView();
            try
            {
                var resp = PerformMove(view);
                var responseMove = _service.PerformMove(null);
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
