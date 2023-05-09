using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    internal class C2M_RemoveUnitHandler : AMActorLocationHandler<Unit, C2M_RemoveUnit>
    {
        protected override async ETTask Run(Unit unit, C2M_RemoveUnit message)
        {

            //if (monsterunit != null)
            //{

            //    monsterunit.RemoveComponent<MoveComponent>();

            //    monsterunit.RemoveComponent<PathfindingComponent>();

            //    //monsterunit.RemoveComponent<MailBoxComponent>();

            //    monsterunit.RemoveComponent<AIComponent>();
            //    // aoi
            //    //monsterunit.RemoveComponent<AOIEntity>();
            //}

            RunAsync(unit, message).Coroutine();
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
        protected async ETTask RunAsync(Unit unit, C2M_RemoveUnit message)
        {
            await ETTask.CompletedTask;
            var levelcomponent = unit.DomainScene().GetComponent<LevelComponent>();
            if (unit.Type == UnitType.Player)
            {
                var unitcomponent = unit.DomainScene().GetComponent<UnitComponent>();
                unitcomponent.Remove(message.Id);
                levelcomponent.SubtractPlayer();
                return;
            }
            levelcomponent.RemoveEnemy(message.Id);
            //monsterunit.RemoveComponent<AIComponent>();
            //await TimerComponent.Instance.WaitAsync(5000);

        }
    }
}
