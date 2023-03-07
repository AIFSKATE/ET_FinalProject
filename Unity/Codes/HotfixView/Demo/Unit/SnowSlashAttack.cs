using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    public class SnowSlashAttack : AEvent<EventType.SnowSlash>
    {
        protected override void Run(SnowSlash args)
        {
            RunAsync(args).Coroutine();
        }

        protected async ETTask RunAsync(SnowSlash args)
        {
            Unit unit = args.Unit;
            var mygameobject = unit.GetComponent<GameObjectComponent>().GameObject;
            GameObject SnowSlashVFS = RecyclePoolComponent.Instance.Get("SnowSlash");
            SnowSlashVFS.transform.SetParent(mygameobject.transform, false);

            ////范围检测代码
            var position = SnowSlashVFS.transform.position;
            Vector3 vector3 = new Vector3(2f, 0.5f, 0.5f);
            position += 1.5f * SnowSlashVFS.transform.forward;
            Collider[] colliders = Physics.OverlapBox(position, vector3, SnowSlashVFS.transform.rotation);

            ////Test，测试范围检测代码的范围是什么
            //var g = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //g.transform.position = position;
            //g.transform.localScale = vector3;
            //g.transform.rotation = SnowSlashVFS.transform.rotation;

            await TimerComponent.Instance.WaitAsync(750);
            //回收
            RecyclePoolComponent.Instance.Recycle(SnowSlashVFS);
            List<long> list = new List<long>();
            foreach (var item in colliders)
            {
                list.Add(item.GetComponent<DelegateCollider>().BelongToUnitId);
            }
            //如果没有打中就不要发送这个消息，为了测试能否发送的话可以把if先注释掉
            if (list.Count > 0)
            {
                args.session.Send(new C2M_DamageMonsters() { ids = list });
            }
            await ETTask.CompletedTask;
        }
    }
}
