using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TheEmpire.Client.DTO
{
    //AuthString := TeamName + ‘:’ + ClientName + ‘:’ + SessionId + ‘:’ +
    //SequenceNumber
    //AuthCode := GetAuthCode(AuthString)
    //GetAuthCode(Payload):
    //SignBytes := GetUTF8Bytes(Payload + Secret)
    //Return LowercaseHex(SHA1(SignBytes))
    public class ReqAuth
    {
        public string TeamName { get; set; }
        public string ClientName { get; set; }
        public int SessionId { get; set; }
        public int SequenceNumber { get; set; }
        public string AuthCode
        {
            get
            {
                var secret = ConfigurationManager.AppSettings["AuthSecret"];
                var authString = $"{TeamName}:{ClientName}:{SessionId}:{SequenceNumber}{secret}";
                return ComputeSha1(authString);
            }
        }

        private static string ComputeSha1(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
