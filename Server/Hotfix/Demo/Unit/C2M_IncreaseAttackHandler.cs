using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    internal class C2M_IncreaseAttackHandler : AMActorLocationHandler<Unit, C2M_IncreaseAttack>
    {
        protected override async ETTask Run(Unit unit, C2M_IncreaseAttack message)
        {
            MessageHelper.Broadcast(unit, new M2C_IncreaseAttack() { Id = unit.Id });
            await ETTask.CompletedTask;
        }
    }
}

