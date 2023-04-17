using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [MessageHandler]
    internal class M2C_AnimatorTriggerHandler : AMHandler<M2C_AnimatorTrigger>
    {
        protected override void Run(Session session, M2C_AnimatorTrigger message)
        {
            Scene currentScene = session.DomainScene().CurrentScene();
            UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
            Unit unit = unitComponent.Get(message.Id);
            Game.EventSystem.Publish<AnimatorTrigger>(new AnimatorTrigger()
            {
                Unit = unit,
                session = session,
                trigger = message.trigger,
            });
        }
    }
}
