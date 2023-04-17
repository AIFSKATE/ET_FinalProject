using NLog.Targets;
using UnityEngine;

namespace ET
{
    public class AI_XunLuo : AAIHandler
    {
        public override int Check(AIComponent aiComponent, AIConfig aiConfig)
        {
            var unitlist = aiComponent.DomainScene().GetComponent<UnitComponent>().GetPlauerList();
            Unit unit = aiComponent.Parent as Unit;
            Unit myunit = aiComponent.Parent as Unit;

            int index = (int)((myunit.Id / 2) & 15) % unitlist.Count;
            unit = unitlist[index];

            UnityEngine.Vector3 targetposition = unit.Position;
            UnityEngine.Vector3 myposition = myunit.Position;
            if (Vector3.Distance(targetposition, myposition) > 5)
            {
                return 0;
            }
            return 1;
        }

        public override async ETTask Execute(AIComponent aiComponent, AIConfig aiConfig, ETCancellationToken cancellationToken)
        {
            var unitlist = aiComponent.DomainScene().GetComponent<UnitComponent>().GetPlauerList();
            Unit unit = aiComponent.Parent as Unit;
            Unit myunit = aiComponent.Parent as Unit;

            int index = (int)((myunit.Id / 2) & 15) % unitlist.Count;
            unit = unitlist[index];

            myunit.FindPathMoveToAsync(unit.Position, cancellationToken).Coroutine();

            MessageHelper.Broadcast(unit, new M2C_AnimatorTrigger()
            {
                Id = myunit.Id,
                trigger = "Walk",
            });

            await ETTask.CompletedTask;
        }
    }
}