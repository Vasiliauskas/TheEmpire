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
        private string _serverUrl;

        public ClientService(string serverUrl)
        {
            this._serverUrl = serverUrl;
        }

        public CreatePlayerResp CreatePlayer()
        {
            var addr = _serverUrl+"/ClientService.svc/json/CreatePlayer";

            var createPlayerRequest = new CreatePlayerReq()
            {
                Auth = new ReqAuth() { ClientName = "test", SequenceNumber = 1, SessionId = 4564, TeamName = "The Empire" }
            };
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(createPlayerRequest);

            var response = RestHelper.SendPost(new Uri(addr), data);
            JsonSerializer serializer = new JsonSerializer();

            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                return (CreatePlayerResp)serializer.Deserialize(streamReader, typeof(CreatePlayerResp));
            }
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
