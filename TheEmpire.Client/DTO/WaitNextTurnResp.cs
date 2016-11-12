using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEmpire.Client.DTO
{
    public class WaitNextTurnResp : BaseResp
    {
        public bool TurnComplete;
        public bool GameFinished;
        public string FinishCondition;
        public string FinishComment;
        public bool YourTurn;
    }

    public class GetPlayerViewReq : BaseReq
    {
        public int PlayerId;
    }
    public class GetPlayerViewResp : BaseResp
    {
        public int Turn;
        public string Mode;
        public EnMapData Map;
        public EnPoint TecmanPosition;
        public List<EnPoint> GhostPositions;
        public List<EnPoint> PreviousTecmanPosition;
        public string Status;
        public string Message;
    }
    public class PerformMoveRequest : BaseReq
    {
        public int PlayerId;
        public List<Position> Positions;
    }
    public class BaseResp
    {
        public string Status;
        public string Message;
    }

    public class BaseReq
    {
        public ReqAuth Auth;
    }

    public class PerformMoveResponse : BaseResp
    {
        public string Status;
        public string Message;
    }

}
