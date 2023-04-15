using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [MessageHandler]
    internal class M2C_DragonPunchHandler : AMHandler<M2C_DragonPunch>
    {
        protected override void Run(Session session, M2C_DragonPunch message)
        {
            Scene currentScene = session.DomainScene().CurrentScene();
            UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
            Unit unit = unitComponent.Get(message.Id);
            Game.EventSystem.Publish<DragonPunch>(new DragonPunch()
            {
                Unit = unit,
                session = session,
                Forward = new UnityEngine.Vector3(message.X, message.Y, message.Z)
            });
        }
    }
}
