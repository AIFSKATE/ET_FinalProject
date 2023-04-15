using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [MessageHandler]
    internal class M2C_IncreaseAttackHandler : AMHandler<M2C_IncreaseAttack>
    {
        protected override void Run(Session session, M2C_IncreaseAttack message)
        {
            Scene currentScene = session.DomainScene().CurrentScene();
            UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
            Unit unit = unitComponent.Get(message.Id);
            Game.EventSystem.Publish<IncreaseAttack>(new IncreaseAttack() { Unit = unit, session = session });
        }
    }
}
