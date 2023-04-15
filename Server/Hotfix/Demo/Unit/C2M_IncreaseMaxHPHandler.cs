using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    internal class C2M_IncreaseMaxHPHandler : AMActorLocationHandler<Unit, C2M_IncreaseMaxHP>
    {
        protected override async ETTask Run(Unit unit, C2M_IncreaseMaxHP message)
        {
            MessageHelper.Broadcast(unit, new M2C_IncreaseMaxHP() { Id = unit.Id });
            await ETTask.CompletedTask;
        }
    }
}

