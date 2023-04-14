using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [MessageHandler]
    internal class M2C_MeteorsAOEHandler : AMHandler<M2C_MeteorsAOE>
    {
        protected override void Run(Session session, M2C_MeteorsAOE message)
        {
            Scene currentScene = session.DomainScene().CurrentScene();
            UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
            Unit unit = unitComponent.Get(message.Id);
            Game.EventSystem.Publish<MeteorsAOE>(new MeteorsAOE()
            {
                Unit = unit,
                session = session,
                Position = new UnityEngine.Vector3(message.X, message.Y, message.Z)
            });
        }
    }
}
