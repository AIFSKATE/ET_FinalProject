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
            UnitComponent unitComponent = currentscene.GetComponent<UnitComponent>();
            UnitConfig unitConfig = UnitConfigCategory.Instance.Get(1002);
            for (int i = 0; i < 4; i++)
            {
                Unit unitenemy = UnitFactory.Create(currentscene, unitConfig.Id, UnitType.Monster);
                unitComponent.Add(unitenemy);
            }
            reply();
            await ETTask.CompletedTask;
        }
    }
}
