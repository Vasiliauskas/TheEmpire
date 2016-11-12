using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEmpire.Client.DTO
{
    public class ReqAuth
    {
        public string TeamName;
        public string ClientName;
        public int SessionId;
        public int SequenceNumber;
        public string AuthCode;
    }
}
