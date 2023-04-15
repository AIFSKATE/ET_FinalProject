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
    public class DragonPunchAttack : AEvent<EventType.DragonPunch>
    {
        protected override void Run(DragonPunch args)
        {
            RunAsync(args).Coroutine();
        }

        protected async ETTask RunAsync(DragonPunch args)
        {
            Unit unit = args.Unit;
            GameObject spinningChopVFS = RecyclePoolComponent.Instance.GetUnit("Dragon punch");
            Vector3 temp = unit.Position;
            temp.y += 0.5f;
            spinningChopVFS.transform.position = temp;

            args.Forward.y = 0;
            spinningChopVFS.transform.forward = args.Forward;

            //范围检测代码
            var position = spinningChopVFS.transform.position;
            position += 3f * spinningChopVFS.transform.forward;
            Vector3 extends = new Vector3(0.75f, 0.75f, 3);
            Collider[] colliders = Physics.OverlapBox(position, extends, spinningChopVFS.transform.rotation);

            //Test，测试范围检测代码的范围是什么
            //var g = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //var position = spinningChopVFS.transform.position;
            //position += 3f * spinningChopVFS.transform.forward;
            //g.transform.position = position;
            //g.transform.rotation = spinningChopVFS.transform.rotation;
            //g.transform.localScale = new Vector3(1.5f, 1.5f, 6);

            List<long> list = new List<long>();
            foreach (var item in colliders)
            {
                if (item.tag == "Animal" && item.GetComponent<DelegateMonoBehaviour>() != null)
                {
                    list.Add(item.GetComponent<DelegateMonoBehaviour>().BelongToUnitId);
                }
            }
            //如果没有打中就不要发送这个消息，为了测试能否发送的话可以把if先注释掉
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
                    Game.EventSystem.PublishAsync<Damage>(new Damage() { Unit = tempunit, damage = damage * 2 }).Coroutine();
                }
            }

            //回收
            await TimerComponent.Instance.WaitAsync(1000);
            RecyclePoolComponent.Instance.Recycle(spinningChopVFS);

            await ETTask.CompletedTask;
        }


    }
}
