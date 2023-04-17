using UnityEngine;

namespace ET
{
    public class AI_Locate : AAIHandler
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
            if (Vector3.Distance(targetposition, myposition) > 3)
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

            int ran = RandomHelper.RandomNumber(0, 360);
            float sin = Mathf.Sin(ran);
            float cos = Mathf.Cos(ran);

            var randompos = unit.Position + UnityEngine.Vector3.right * sin + UnityEngine.Vector3.forward * cos;

            myunit.FindPathMoveToAsync(randompos, cancellationToken).Coroutine();

            MessageHelper.Broadcast(unit, new M2C_AnimatorTrigger()
            {
                Id = myunit.Id,
                trigger = "Walk",
            });

            await ETTask.CompletedTask;
        }
    }
}