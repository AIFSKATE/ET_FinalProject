using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    internal class C2M_HuiXueHandler : AMActorLocationHandler<Unit, C2M_HuiXue>
    {
        protected override async ETTask Run(Unit unit, C2M_HuiXue message)
        {
            MessageHelper.Broadcast(unit, new M2C_HuiXue() { Id = unit.Id });
            await ETTask.CompletedTask;
        }
    }
}

