using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [ActorMessageHandler]
    internal class C2M_StartlevelHandler : AMActorLocationRpcHandler<Unit, C2M_Startlevel, M2C_Startlevel>
    {
        protected override async ETTask Run(Unit unit, C2M_Startlevel request, M2C_Startlevel response, Action reply)
        {
            Scene currentscene = unit.DomainScene();
            var levelComponent = currentscene.GetComponent<LevelComponent>();
            if (request.nowlevel > levelComponent.GetEndLevel())
            {
                response.Error = ErrorCode.ERR_LevelEND;
                reply();
                return;
            }
            levelComponent.StartLevel(request.nowlevel);
            reply();
            await ETTask.CompletedTask;
        }
    }
}
