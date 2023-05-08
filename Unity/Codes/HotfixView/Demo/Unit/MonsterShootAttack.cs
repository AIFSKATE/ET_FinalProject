using DG.Tweening;
using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

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
            var monstergameobject = monster.GetComponent<GameObjectComponent>().GameObject;

            GameObject Projectile = RecyclePoolComponent.Instance.GetUnit("Projectile");
            Projectile.transform.forward = monstergameobject.transform.forward;
            Projectile.transform.position = monstergameobject.transform.position;
            Projectile.GetComponent<DelegateMonoBehaviour>().Clear();
            Projectile.GetComponent<DelegateMonoBehaviour>().on_TriggerEnter += TriggerEnter;
            Projectile.GetComponent<Rigidbody>().velocity = Projectile.transform.forward * 3;

            LookAt(monster, player.GetComponent<GameObjectComponent>().GameObject.transform).Coroutine();
            await TimerComponent.Instance.WaitAsync(2500);
            //回收
            if (!RecyclePoolComponent.Instance.AlreaHave(Projectile))
            {
                Projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
                RecyclePoolComponent.Instance.Recycle(Projectile);
            }

            void TriggerEnter(Collider collider)
            {
                var targetdele = collider.GetComponent<DelegateMonoBehaviour>();
                if (collider.tag == "Player" && targetdele != null && monster != null)
                {
                    var unitComponent = monster?.DomainScene()?.GetComponent<UnitComponent>();
                    var unit = unitComponent?.Get(targetdele.BelongToUnitId);
                    int realdamage = 3 - player.GetComponent<MainRoleComponent>().GetNum((int)NumType.defense);
                    realdamage = realdamage > 0 ? realdamage : 0;
                    unit?.GetComponent<MainRoleComponent>().ChangeNum((int)NumType.hp, -realdamage);
                    if (!RecyclePoolComponent.Instance.AlreaHave(Projectile))
                    {
                        Projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        RecyclePoolComponent.Instance.Recycle(Projectile);
                    }
                }
            }
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
