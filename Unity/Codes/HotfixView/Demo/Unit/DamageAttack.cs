using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    public class DamageAttack : AEventAsync<EventType.Damage>
    {
        protected override async ETTask Run(Damage args)
        {
            args.Unit.GetComponent<HPComponent>().GetDamage(args.damage);
            await ETTask.CompletedTask;
        }
    }
}
