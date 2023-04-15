using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [MessageHandler]
    internal class M2C_IncreaseMaxHPHandler : AMHandler<M2C_IncreaseMaxHP>
    {
        protected override void Run(Session session, M2C_IncreaseMaxHP message)
        {
            Scene currentScene = session.DomainScene().CurrentScene();
            UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
            Unit unit = unitComponent.Get(message.Id);
            Game.EventSystem.Publish<IncreaseMaxHP>(new IncreaseMaxHP() { Unit = unit, session = session });
        }
    }
}
