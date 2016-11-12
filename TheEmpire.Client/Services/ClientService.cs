using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TheEmpire.Client.DTO;

namespace TheEmpire.Client
{
    class ClientService
    {
        private static readonly Random _random = new Random();

        private string _serverUrl;
        private readonly string _teamName;
        private readonly int _sessionId;
        private int _seqNumber;

        public ClientService(string serverUrl)
        {
            this._serverUrl = serverUrl;
            _teamName = ConfigurationManager.AppSettings["TeamName"];

            var rnd = new Random(DateTime.Now.Millisecond/3);
            _sessionId = rnd.Next();
        }

        public CreatePlayerResp CreatePlayer()
        {
            var addr = _serverUrl+"/ClientService.svc/json/CreatePlayer";
            var createPlayerRequest = new CreatePlayerReq()
            {
                Auth = new ReqAuth() { ClientName = "TheEmpireClient", SequenceNumber = 1, SessionId = _sessionId, TeamName = _teamName }
            };
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(createPlayerRequest);
            var response = RestHelper.SendPost(new Uri(addr), data);
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                return (CreatePlayerResp)serializer.Deserialize(streamReader, typeof(CreatePlayerResp));
            }
        }

        private int GetSequenceNumber()
        {
            return _seqNumber++;
        }

        public WaitNextTurnResp WaitNextTurn(int playerId, int refTurn)
        {
            var addr = _serverUrl + "/ClientService.svc/json/WaitNextTurn";
            var createPlayerRequest = new WaitNextTurnReq()
            {
                PlayerId = playerId,
                RefTurn = refTurn,
                Auth = new ReqAuth() { ClientName = "TheEmpireClient", SequenceNumber = 2, SessionId = _sessionId, TeamName = _teamName }
            };
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(createPlayerRequest);
            var response = RestHelper.SendPost(new Uri(addr), data);
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
        {
                return (WaitNextTurnResp)serializer.Deserialize(streamReader, typeof(WaitNextTurnResp));
            }
        }

        public bool GetPlayerView()
        {
            throw new NotImplementedException();

        }

        public bool PerformMove()
        {
            throw new NotImplementedException();
        }
    }
}
