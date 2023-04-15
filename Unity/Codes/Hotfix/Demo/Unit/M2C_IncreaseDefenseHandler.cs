using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [MessageHandler]
    internal class M2C_IncreaseDefenseHandler : AMHandler<M2C_IncreaseDefense>
    {
        protected override void Run(Session session, M2C_IncreaseDefense message)
        {
            Scene currentScene = session.DomainScene().CurrentScene();
            UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
            Unit unit = unitComponent.Get(message.Id);
            Game.EventSystem.Publish<IncreaseDefense>(new IncreaseDefense() { Unit = unit, session = session });
        }
    }
}
