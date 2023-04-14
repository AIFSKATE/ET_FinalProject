using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    internal class C2M_RemoveUnitHandler : AMActorLocationHandler<Unit, C2M_RemoveUnit>
    {
        protected override async ETTask Run(Unit unit, C2M_RemoveUnit message)
        {
            var unitcomponent = unit.DomainScene().GetComponent<UnitComponent>();
            unitcomponent.Remove(message.Id);
            await ETTask.CompletedTask;
        }
    }
}
