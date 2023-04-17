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
    public class SpinningChopAttack : AEvent<EventType.SpinningChop>
    {
        protected override void Run(SpinningChop args)
        {
            RunAsync(args).Coroutine();
        }

        protected async ETTask RunAsync(SpinningChop args)
        {
            Unit unit = args.Unit;
            GameObject spinningChopVFS = RecyclePoolComponent.Instance.GetUnit("Portal yellow");
            Vector3 temp = unit.Position;
            temp.y += 0.5f;
            spinningChopVFS.transform.position = temp;

            args.Forward.y = 0;

            spinningChopVFS.GetComponent<Rigidbody>().velocity = args.Forward * 6;
            spinningChopVFS.GetComponent<DelegateMonoBehaviour>().Clear();
            spinningChopVFS.GetComponent<DelegateMonoBehaviour>().on_TriggerEnter += SpinningChopAttack_on_TriggerEnter;

            void SpinningChopAttack_on_TriggerEnter(Collider obj)
            {
                if (obj.tag == "Animal" && obj.GetComponent<DelegateMonoBehaviour>() != null)
                {
                    List<long> list = new List<long>();
                    var id = obj.GetComponent<DelegateMonoBehaviour>().BelongToUnitId;
                    list.Add(id);

                    int damage = unit.GetComponent<MainRoleComponent>().GetNum((int)NumType.damage);
                    args.session.Send(new C2M_DamageMonsters()
                    {
                        ids = list,
                        damage = damage,
                        damagetype = (int)DamageType.SpinningChop,
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

            }

            //回收
            await TimerComponent.Instance.WaitAsync(5000);
            RecyclePoolComponent.Instance.Recycle(spinningChopVFS);

            await ETTask.CompletedTask;
        }


    }
}
