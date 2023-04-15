using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    internal class C2M_DragonPunchHandler : AMActorLocationHandler<Unit, C2M_DragonPunch>
    {
        protected override async ETTask Run(Unit unit, C2M_DragonPunch message)
        {
            MessageHelper.Broadcast(unit, new M2C_DragonPunch()
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

