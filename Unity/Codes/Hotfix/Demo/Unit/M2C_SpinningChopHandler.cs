using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [MessageHandler]
    internal class M2C_SpinningChopHandler : AMHandler<M2C_SpinningChop>
    {
        protected override void Run(Session session, M2C_SpinningChop message)
        {
            Scene currentScene = session.DomainScene().CurrentScene();
            UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
            Unit unit = unitComponent.Get(message.Id);
            Game.EventSystem.Publish<SpinningChop>(new SpinningChop()
            {
                Unit = unit,
                session = session,
                Forward = new UnityEngine.Vector3(message.X, message.Y, message.Z)
            });
        }
    }
}
