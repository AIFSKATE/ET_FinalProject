using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [MessageHandler]
    internal class M2C_HuiXueHandler : AMHandler<M2C_HuiXue>
    {
        protected override void Run(Session session, M2C_HuiXue message)
        {
            Scene currentScene = session.DomainScene().CurrentScene();
            UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
            Unit unit = unitComponent.Get(message.Id);
            Game.EventSystem.Publish<HuiXue>(new HuiXue() { Unit = unit, session = session });
        }
    }
}
