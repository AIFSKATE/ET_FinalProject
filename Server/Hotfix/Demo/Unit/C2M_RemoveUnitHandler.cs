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

            //if (unitcomponent.GetPlauerList().Count == unitcomponent.Children.Count)
            //{
            //    Scene currentscene = unit.DomainScene();
            //    var levelComponent = currentscene.GetComponent<LevelComponent>();
            //    if (levelComponent.GetEndLevel() < levelComponent.GetNowLevel())
            //    {
            //        MessageHelper.Broadcast(unit, new M2C_EndLevel());
            //        return;
            //    }
            //    levelComponent.NextLevel();
            //}

            await ETTask.CompletedTask;
        }
    }
}
