using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [MessageHandler]
    internal class M2C_EndLevelHandler : AMHandler<M2C_EndLevel>
    {
        protected override void Run(Session session, M2C_EndLevel message)
        {
            Scene currentScene = session.DomainScene().CurrentScene();
            LevelComponent levelComponent = currentScene.GetComponent<LevelComponent>();
            levelComponent.EndLevel().Coroutine();
        }
    }
}
