using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheEmpire.Client.DTO;

namespace TheEmpire.Client.Services
{
    public class GameService
    {
        private readonly ClientService _client;
        private int _playerId;

        public GameService(string url)
        {
            _client = new ClientService(url);
        }

        public WaitNextTurnResp WaitNextTurn(int refTurn = 0)
        {
            return _client.WaitNextTurn(_playerId, refTurn);
        }

        public GetPlayerViewResp GetPlayerView()
        {
            return _client.GetPlayerView(_playerId);
        }

        public void CreatePlayer()
        {
            var player = _client.CreatePlayer();
            Console.WriteLine($"Got player id: {player.PlayerId} status {player.Status} message {player.Message}");
            _playerId = player.PlayerId;
        }

        public PerformMoveResponse PerformMove(IEnumerable<Position> positions)
        {
            return _client.PerformMove(_playerId, positions);
        }
    }
}
