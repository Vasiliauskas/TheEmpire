<<<<<<< HEAD
﻿using Newtonsoft.Json;
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
        private readonly string _clientName;

        public ClientService(string serverUrl)
        {
            this._serverUrl = serverUrl;
            _teamName = ConfigurationManager.AppSettings["TeamName"];
            _clientName = ConfigurationManager.AppSettings["ClientName"];
            var rnd = new Random(DateTime.Now.Millisecond / 3);
            _sessionId = rnd.Next();
        }

        public CreatePlayerResp CreatePlayer()
        {
            var addr = _serverUrl + "/ClientService.svc/json/CreatePlayer";
            var createPlayerRequest = new CreatePlayerReq()
            {
                Auth = GetAuth()
            };
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(createPlayerRequest);
            var response = RestHelper.SendPost(new Uri(addr), data);
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                return (CreatePlayerResp)serializer.Deserialize(streamReader, typeof(CreatePlayerResp));
            }
        }

        private ReqAuth GetAuth()
        {
            return new ReqAuth()
            {
                ClientName = _clientName,
                SequenceNumber = GetSequenceNumber(),
                SessionId = _sessionId,
                TeamName = _teamName
            };
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
                Auth = GetAuth()
            };
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(createPlayerRequest);
            var response = RestHelper.SendPost(new Uri(addr), data);
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                return (WaitNextTurnResp)serializer.Deserialize(streamReader, typeof(WaitNextTurnResp));
            }
        }

        public GetPlayerViewResp GetPlayerView(int playerId)
        {
            var addr = _serverUrl + "/ClientService.svc/json/GetPlayerViewn";
            var createPlayerRequest = new GetPlayerViewReq()
            {
                PlayerId = playerId,
                Auth = GetAuth()
            };
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(createPlayerRequest);
            var response = RestHelper.SendPost(new Uri(addr), data);
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                return (GetPlayerViewResp)serializer.Deserialize(streamReader, typeof(GetPlayerViewResp));
            }
        }

        public PerformMoveResponse PerformMove(int playerId, IEnumerable<Position> positions)
        {
            var addr = _serverUrl + "/ClientService.svc/json/PerformMove";
            var createPlayerRequest = new PerformMoveRequest()
            {
                PlayerId = playerId,
                Positions = positions.ToList(),
                Auth = GetAuth()
            };
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(createPlayerRequest);
            var response = RestHelper.SendPost(new Uri(addr), data);
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                return (PerformMoveResponse)serializer.Deserialize(streamReader, typeof(PerformMoveResponse));
            }
        }
    }
}
=======
﻿using Newtonsoft.Json;
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
>>>>>>> 1bec934cbe92554d659fe6edf128ab7462d59870
