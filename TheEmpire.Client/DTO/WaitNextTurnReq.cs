﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEmpire.Client.DTO
{
    class WaitNextTurnReq : BaseReq
    {
        int PlayerId;
        int RefTurn;
    }
}
