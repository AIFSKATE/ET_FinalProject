using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    [MessageHandler]
    internal class M2C_DamageMonstersHandler : AMHandler<M2C_DamageMonsters>
    {
        protected override void Run(Session session, M2C_DamageMonsters message)
        {
            if (message.id == session.ZoneScene().GetComponent<PlayerComponent>().MyId)
            {
                return;
            }
            Scene currentScene = session.DomainScene().CurrentScene();
            UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
            foreach (var item in message.ids)
            {
                //unitComponent.Remove(item);
                Unit unit = unitComponent.Get(item);
                Game.EventSystem.PublishAsync<Damage>(new Damage() { Unit = unit, damage = message.damage }).Coroutine();
            }
        }
    }
}

