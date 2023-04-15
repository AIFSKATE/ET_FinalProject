using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    public class IncreaseDefenseSupplies : AEvent<EventType.IncreaseDefense>
    {
        protected override void Run(IncreaseDefense args)
        {
            RunAsync(args).Coroutine();
        }

        protected async ETTask RunAsync(IncreaseDefense args)
        {
            Unit unit = args.Unit;
            var mygameobject = unit.GetComponent<GameObjectComponent>().GameObject;
            GameObject HealingVFS = RecyclePoolComponent.Instance.GetUnit("Buff");
            HealingVFS.transform.SetParent(mygameobject.transform, false);

            unit.GetComponent<MainRoleComponent>().ChangeNum((int)NumType.defense, 10);

            await TimerComponent.Instance.WaitAsync(2500);
            //回收
            RecyclePoolComponent.Instance.Recycle(HealingVFS);

            await ETTask.CompletedTask;
        }
    }
}
