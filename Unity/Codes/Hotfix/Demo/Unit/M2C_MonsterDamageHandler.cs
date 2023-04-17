using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [MessageHandler]
    internal class M2C_MonsterDamageHandler : AMHandler<M2C_MonsterDamage>
    {
        protected override void Run(Session session, M2C_MonsterDamage message)
        {
            Scene currentScene = session.DomainScene().CurrentScene();
            UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
            Unit playerunit = unitComponent.Get(message.PlayerId);
            Unit monsterunit = unitComponent.Get(message.MonsterId);
            Game.EventSystem.Publish<MonsterDamage>(new MonsterDamage()
            {
                PlayerUnit = playerunit,
                MonsterUnit = monsterunit,
                session = session,
                damage = message.damage
            });
        }
    }
}
