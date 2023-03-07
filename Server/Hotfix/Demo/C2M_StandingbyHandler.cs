using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [ActorMessageHandler]
    internal class C2M_StandingbyHandler : AMActorLocationRpcHandler<Unit, C2M_Standingby, M2C_Standingby>
    {
        protected override async ETTask Run(Unit unit, C2M_Standingby request, M2C_Standingby response, Action reply)
        {
            reply();
            await ETTask.CompletedTask;
        }
    }
}
