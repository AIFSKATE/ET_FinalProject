using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    internal class C2M_RemoveAllEnemyUnitHandler : AMActorLocationHandler<Unit, C2M_RemoveAllEnemyUnit>
    {
        protected override async ETTask Run(Unit unit, C2M_RemoveAllEnemyUnit message)
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
        protected async ETTask RunAsync(Unit unit, C2M_RemoveAllEnemyUnit message)
        {
            await ETTask.CompletedTask;
            var unitcomponent = unit.DomainScene().GetComponent<UnitComponent>();
            var levelcomponent = unit.DomainScene().GetComponent<LevelComponent>();
            levelcomponent.Clear();

            var monsterunit = unitcomponent.Get(message.Id);
            int count = unitcomponent.Children.Count;
            List<long> tidlist = new List<long>();
            foreach (var item in unitcomponent.Children)
            {
                if ((item.Value as Unit).Type != UnitType.Player)
                {
                    tidlist.Add(item.Key);
                }
            }
            foreach (var item in tidlist)
            {
                unitcomponent.Remove(item);
            }
            tidlist.Clear();
            tidlist = null;
            //monsterunit.RemoveComponent<AIComponent>();
            //await TimerComponent.Instance.WaitAsync(5000);
            //unitcomponent.Remove(message.Id);
        }
    }
}
