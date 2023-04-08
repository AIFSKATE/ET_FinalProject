using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    internal class C2M_DamageMonstersHandler : AMActorLocationHandler<Unit, C2M_DamageMonsters>
    {
        protected override async ETTask Run(Unit unit, C2M_DamageMonsters message)
        {
            MessageHelper.Broadcast(unit, new M2C_DamageMonsters() { ids = message.ids, damage = message.damage });
            await ETTask.CompletedTask;
        }
    }
}
