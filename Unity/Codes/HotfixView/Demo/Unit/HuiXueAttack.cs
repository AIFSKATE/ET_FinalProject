using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    public class HuiXueAttack : AEvent<EventType.HuiXue>
    {
        protected override void Run(HuiXue args)
        {
            RunAsync(args).Coroutine();
        }

        protected async ETTask RunAsync(HuiXue args)
        {
            Unit unit = args.Unit;
            var mygameobject = unit.GetComponent<GameObjectComponent>().GameObject;
            GameObject HealingVFS = RecyclePoolComponent.Instance.GetUnit("Healing");
            HealingVFS.transform.SetParent(mygameobject.transform, false);


            await TimerComponent.Instance.WaitAsync(2500);
            //回收
            RecyclePoolComponent.Instance.Recycle(HealingVFS);

            await ETTask.CompletedTask;
        }
    }
}
