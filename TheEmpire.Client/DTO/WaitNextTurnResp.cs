using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEmpire.Client.DTO
{
    class WaitNextTurnResp : BaseResp
    {
        bool TurnComplete;
        bool GameFinished;
        string FinishCondition;
        string FinishComment;
        bool YourTurn;
    }

    class GetPlayerViewReq : BaseReq
    {
        int PlayerId;
    }
    class GetPlayerViewResp : BaseResp
    {
        int Turn;
        string Mode;
        EnMapData Map;
        EnPoint TecmanPosition;
        List<EnPoint> GhostPositions;
        List<EnPoint> PreviousTecmanPosition;
        string Status;
        string Message;
    }
  
    class PerformMoveRequest : BaseReq
    {
        int PlayerId;
        List<Position> Positions;
    }
    class BaseResp
    {
        string Status;
        string Message;
    }

    class BaseReq
    {
        ReqAuth Auth;
    }

    class PerformMoveResponse : BaseResp
    {
        string Status;
        string Message;
    }

}
