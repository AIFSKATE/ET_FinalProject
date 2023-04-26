using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [MessageHandler]
    internal class M2C_PrepareTheNextHandler : AMHandler<M2C_PrepareTheNext>
    {
        protected override void Run(Session session, M2C_PrepareTheNext message)
        {
            Scene currentScene = session.DomainScene().CurrentScene();
            LevelComponent levelComponent = currentScene.GetComponent<LevelComponent>();
            Game.EventSystem.Publish(new PrepareTheNext() { ZoneScene = session.ZoneScene(), time = message.time });
        }
    }
}
