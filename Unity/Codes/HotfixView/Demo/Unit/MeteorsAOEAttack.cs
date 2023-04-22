using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    public class MeteorsAOEAttack : AEvent<EventType.MeteorsAOE>
    {
        protected override void Run(MeteorsAOE args)
        {
            RunAsync(args).Coroutine();
        }

        protected async ETTask RunAsync(MeteorsAOE args)
        {
            Unit unit = args.Unit;
            GameObject meteorsAOEVFS = RecyclePoolComponent.Instance.GetUnit("MeteorsAOE");
            args.Position.y += 1;
            meteorsAOEVFS.transform.position = args.Position;

            //范围检测代码
            var position = meteorsAOEVFS.transform.position;
            position += 1.5f * meteorsAOEVFS.transform.forward;
            //Debug.LogWarning("检测前");

            Collider[] colliders = Physics.OverlapSphere(position, 1.7f);
            //Debug.LogWarning("检测中" + colliders.Length);
            //Test，测试范围检测代码的范围是什么
            //var g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //g.transform.position = meteorsAOEVFS.transform.position;
            //g.transform.rotation = meteorsAOEVFS.transform.rotation;
            //g.transform.localScale = new Vector3(2, 2, 2);


            List<long> list = new List<long>();
            foreach (var item in colliders)
            {
                if (item.tag == "Animal" && item.GetComponent<DelegateMonoBehaviour>() != null)
                {
                    list.Add(item.GetComponent<DelegateMonoBehaviour>().BelongToUnitId);
                    //Debug.LogWarning("MeteorsAOEAttack 进入");
                }
            }
            ////如果没有打中就不要发送这个消息，为了测试能否发送的话可以把if先注释掉
            if (list.Count > 0)
            {
                int damage = unit.GetComponent<MainRoleComponent>().GetNum((int)NumType.damage);
                args.session.Send(new C2M_DamageMonsters()
                {
                    ids = list,
                    damage = damage,
                    damagetype = (int)DamageType.MeteorsAOE,
                });

                var unitComponent = unit.DomainScene().GetComponent<UnitComponent>();
                foreach (var item in list)
                {
                    Unit tempunit = unitComponent.Get(item);
                    if (tempunit != null)
                    {
                        Game.EventSystem.PublishAsync<Damage>(new Damage() { Unit = tempunit, damage = damage }).Coroutine();
                    }
                }
            }

            //回收
            await TimerComponent.Instance.WaitAsync(3000);
            RecyclePoolComponent.Instance.Recycle(meteorsAOEVFS);

            await ETTask.CompletedTask;
        }
    }
}
