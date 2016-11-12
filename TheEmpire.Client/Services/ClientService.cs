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
        private int _seqNumber = 1;
        private readonly int _sessionId = _random.Next(Int32.MaxValue);

        public ClientService(string serverUrl)
        {
            this._serverUrl = serverUrl;
        }

        public CreatePlayerResp CreatePlayer()
        {
            var addr = _serverUrl+"/ClientService.svc/json/CreatePlayer";
            var teamName = ConfigurationManager.AppSettings["TeamName"];

            var createPlayerRequest = new CreatePlayerReq()
            {
                Auth = new ReqAuth() { ClientName = "test1", SequenceNumber = GetSequenceNumber(), SessionId = _sessionId, TeamName = teamName }
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

        public bool WaitNextTurn()
        {
            throw new NotImplementedException();
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
