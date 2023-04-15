using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    public class IncreaseAttackSupplies : AEvent<EventType.IncreaseAttack>
    {
        protected override void Run(IncreaseAttack args)
        {
            RunAsync(args).Coroutine();
        }

        protected async ETTask RunAsync(IncreaseAttack args)
        {
            Unit unit = args.Unit;
            var mygameobject = unit.GetComponent<GameObjectComponent>().GameObject;
            GameObject HealingVFS = RecyclePoolComponent.Instance.GetUnit("Lightning aura");
            HealingVFS.transform.SetParent(mygameobject.transform, false);

            unit.GetComponent<MainRoleComponent>().ChangeNum((int)NumType.damage, 10);

            await TimerComponent.Instance.WaitAsync(2500);
            //回收
            RecyclePoolComponent.Instance.Recycle(HealingVFS);

            await ETTask.CompletedTask;
        }
    }
}
