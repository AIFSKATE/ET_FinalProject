﻿using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    public class IncreaseMaxHPSupplies : AEvent<EventType.IncreaseMaxHP>
    {
        protected override void Run(IncreaseMaxHP args)
        {
            RunAsync(args).Coroutine();
        }

        protected async ETTask RunAsync(IncreaseMaxHP args)
        {
            Unit unit = args.Unit;
            var mygameobject = unit.GetComponent<GameObjectComponent>().GameObject;
            GameObject HealingVFS = RecyclePoolComponent.Instance.GetUnit("Love aura");
            HealingVFS.transform.SetParent(mygameobject.transform, false);

            unit.GetComponent<MainRoleComponent>().ChangeNum((int)NumType.maxhp, 10);

            await TimerComponent.Instance.WaitAsync(2500);
            //回收
            RecyclePoolComponent.Instance.Recycle(HealingVFS);

            await ETTask.CompletedTask;
        }
    }
}
