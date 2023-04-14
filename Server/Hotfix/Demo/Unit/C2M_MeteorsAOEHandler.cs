using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    internal class C2M_MeteorsAOEHandler : AMActorLocationHandler<Unit, C2M_MeteorsAOE>
    {
        protected override async ETTask Run(Unit unit, C2M_MeteorsAOE message)
        {
            MessageHelper.Broadcast(unit, new M2C_MeteorsAOE()
            {
                Id = unit.Id,
                X = message.X,
                Y = message.Y,
                Z = message.Z,
            });
            await ETTask.CompletedTask;
        }
    }
}

