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
            if (Vector3.Distance(targetposition, myposition) <= 1.01)
            {
                return 0;
            }
            return 1;
        }

        public override async ETTask Execute(AIComponent aiComponent, AIConfig aiConfig, ETCancellationToken cancellationToken)
        {
            await ETTask.CompletedTask;
            while (true)
            {
                var unitlist = aiComponent.DomainScene().GetComponent<UnitComponent>().GetPlauerList();
                Unit unit = aiComponent.Parent as Unit;
                Unit myunit = aiComponent.Parent as Unit;

                int index = (int)((myunit.Id / 2) & 15) % unitlist.Count;
                unit = unitlist[index];

                if (myunit != null)
                {
                    MessageHelper.Broadcast(myunit, new M2C_MonsterDamage()
                    {
                        PlayerId = unit.Id,
                        MonsterId = myunit.Id,
                        damage = 5,
                    });
                }

                bool ret = await TimerComponent.Instance.WaitAsync(1000, cancellationToken);
                if (!ret)
                {
                    return;
                }
            }
        }
    }
}