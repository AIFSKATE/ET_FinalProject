using NLog.Targets;
using UnityEngine;

namespace ET
{
    public class AI_XunLuo : AAIHandler
    {
        public override int Check(AIComponent aiComponent, AIConfig aiConfig)
        {
            var unitlist = aiComponent.DomainScene().GetComponent<UnitComponent>().Children;
            Unit unit = aiComponent.Parent as Unit;
            Unit myunit = aiComponent.Parent as Unit;
            foreach (var item in unitlist)
            {
                var temp = item.Value as Unit;
                if (temp.Type == UnitType.Player)
                {
                    unit = temp;
                    break;
                }
            }
            UnityEngine.Vector3 targetposition = unit.Position;
            UnityEngine.Vector3 myposition = myunit.Position;
            if (Vector3.Distance(targetposition, myposition) > 5)
            {
                Log.Debug("你还差得远呢\n\n" + Vector3.Distance(targetposition, myposition) + "\n\n");
                myunit.FindPathMoveToAsync(unit.Position).Coroutine();
                return 0;
            }
            return 1;
        }

        public override async ETTask Execute(AIComponent aiComponent, AIConfig aiConfig, ETCancellationToken cancellationToken)
        {
            //var unitlist = aiComponent.DomainScene().GetComponent<UnitComponent>().Children;
            //Unit unit = aiComponent.Parent as Unit;
            //Unit myunit = aiComponent.Parent as Unit;
            //foreach (var item in unitlist)
            //{
            //    var temp = item.Value as Unit;
            //    if (temp.Type == UnitType.Player)
            //    {
            //        unit = temp;
            //        break;
            //    }
            //}
            //UnityEngine.Vector3 targetposition = unit.Position;
            //UnityEngine.Vector3 myposition = myunit.Position;
            //Scene zoneScene = aiComponent.DomainScene();

            //Unit myUnit = aiComponent.Parent as Unit;
            //if (myUnit == null)
            //{
            //    return;
            //}
            //myunit.FindPathMoveToAsync(unit.Position, cancellationToken).Coroutine();


            await ETTask.CompletedTask;
        }
    }
}