using UnityEngine;

namespace ET
{
    public class AI_Attack : AAIHandler
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
            if (Vector3.Distance(targetposition, myposition) <= 2)
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

            MessageHelper.Broadcast(unit, new M2C_MonsterDamage()
            {
                PlayerId = unit.Id,
                MonsterId = myunit.Id,
                damage = 5,
            });

            await ETTask.CompletedTask;
        }
    }
}