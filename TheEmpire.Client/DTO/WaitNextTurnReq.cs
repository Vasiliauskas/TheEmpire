using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEmpire.Client.DTO
{
    public class WaitNextTurnReq : BaseReq
    {
        public int PlayerId;
        public int RefTurn;
    }
}
