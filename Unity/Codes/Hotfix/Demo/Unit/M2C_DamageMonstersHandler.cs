using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [MessageHandler]
    internal class M2C_DamageMonstersHandler : AMHandler<M2C_DamageMonsters>
    {
        protected override void Run(Session session, M2C_DamageMonsters message)
        {
            Log.Warning("看到这个就证明M2C_DamageMonsters成功下放了");
            Scene currentScene = session.DomainScene().CurrentScene();
            UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
            foreach (var item in message.ids)
            {
                unitComponent.Remove(item);
            }
        }
    }
}

