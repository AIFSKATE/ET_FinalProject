using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    internal class C2M_IncreaseDefenseHandler : AMActorLocationHandler<Unit, C2M_IncreaseDefense>
    {
        protected override async ETTask Run(Unit unit, C2M_IncreaseDefense message)
        {
            MessageHelper.Broadcast(unit, new M2C_IncreaseDefense() { Id = unit.Id });
            await ETTask.CompletedTask;
        }
    }
}

