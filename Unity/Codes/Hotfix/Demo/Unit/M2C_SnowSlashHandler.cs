using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [MessageHandler]
    internal class M2C_SnowSlashHandler : AMHandler<M2C_SnowSlash>
    {
        protected override void Run(Session session, M2C_SnowSlash message)
        {
            Scene currentScene = session.DomainScene().CurrentScene();
            UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
            Unit unit = unitComponent.Get(message.Id);
            Game.EventSystem.Publish<SnowSlash>(new SnowSlash() { Unit = unit, session = session });
        }
    }
}
