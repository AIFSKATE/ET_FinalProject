using DG.Tweening;
using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    public class MonsterShootAttack : AEvent<EventType.MonsterShoot>
    {
        protected override void Run(MonsterShoot args)
        {
            RunAsync(args).Coroutine();
        }
        protected async ETTask RunAsync(MonsterShoot args)
        {
            await ETTask.CompletedTask;
            Unit monster = args.MonsterUnit;
            Unit player = args.PlayerUnit;
            int damage = args.damage;

            monster.GetComponent<AnimatorComponent>().SetTrigger("Shoot");
            LookAt(monster, player.GetComponent<GameObjectComponent>().GameObject.transform).Coroutine();
        
        }

        protected async ETTask LookAt(Unit monster, Transform tgt)
        {
            var thistransf = monster.GetComponent<GameObjectComponent>().GameObject.transform;
            float t = 0;
            Vector3 targetdic = Vector3.back;
            if (thistransf != null)
            {
                targetdic = (tgt.position - thistransf.position).normalized;
            }
            while (t <= 0.25)
            {
                if (thistransf != null)
                {
                    thistransf.forward = Vector3.Lerp(thistransf.forward, targetdic, t * 4);
                    await TimerComponent.Instance.WaitFrameAsync();
                }
                t += Time.deltaTime;
            }
        }
    }
}
