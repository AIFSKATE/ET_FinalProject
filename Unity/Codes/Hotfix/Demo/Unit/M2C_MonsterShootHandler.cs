using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.UI.CanvasScaler;

namespace ET
{
    [MessageHandler]
    internal class M2C_MonsterShootHandler : AMHandler<M2C_MonsterShoot>
    {
        protected override void Run(Session session, M2C_MonsterShoot message)
        {
            Scene currentScene = session.DomainScene().CurrentScene();
            UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
            Unit playerunit = unitComponent.Get(message.PlayerId);
            Unit monsterunit = unitComponent.Get(message.MonsterId);
            Game.EventSystem.Publish<MonsterShoot>(new MonsterShoot()
            {
                PlayerUnit = playerunit,
                MonsterUnit = monsterunit,
                session = session,
                damage = message.damage
            });
        }
    }
}
