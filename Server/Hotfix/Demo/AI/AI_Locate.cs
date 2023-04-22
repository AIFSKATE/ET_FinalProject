﻿using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

namespace ET
{
    public class AI_Locate : AAIHandler
    {
        public override int Check(AIComponent aiComponent, AIConfig aiConfig)
        {
            var unitlist = aiComponent.DomainScene().GetComponent<UnitComponent>().GetPlauerList();
            Unit unit = aiComponent.Parent as Unit;
            Unit myunit = aiComponent.Parent as Unit;

            int index = (int)((myunit.Id / 2) & 15) % unitlist.Count;
            unit = unitlist[index];

            UnityEngine.Vector3 targetposition = unit.Position;
            UnityEngine.Vector3 myposition = myunit.Position;
            if (Vector3.Distance(targetposition, myposition) > 1.5)
            {
                return 0;
            }
            return 1;
        }

        public override async ETTask Execute(AIComponent aiComponent, AIConfig aiConfig, ETCancellationToken cancellationToken)
        {
            await ETTask.CompletedTask;
            while (true)
            {
                var unitlist = aiComponent.DomainScene().GetComponent<UnitComponent>().GetPlauerList();
                Unit unit = aiComponent.Parent as Unit;
                Unit myunit = aiComponent.Parent as Unit;

                int index = (int)((myunit.Id / 2) & 15) % unitlist.Count;
                unit = unitlist[index];

                int ran = RandomHelper.RandomNumber(-6, 7);

                Vector3 straight = (myunit.Position - unit.Position).normalized;
                straight.y = 0;
                Vector3 randompos = unit.Position + Quaternion.CreateFromAxisAngle(Vector3.up, ran / 10f) * straight;

                //var randompos = unit.Position + UnityEngine.Vector3.right * sin + UnityEngine.Vector3.forward * cos;

                myunit.FindPathMoveToAsync(randompos, cancellationToken).Coroutine();

                if (myunit != null)
                {
                    MessageHelper.Broadcast(myunit, new M2C_AnimatorTrigger()
                    {
                        Id = myunit.Id,
                        trigger = "Walk",
                    });
                }
                bool ret = await TimerComponent.Instance.WaitAsync(800, cancellationToken);
                if (!ret)
                {
                    return;
                }
            }
        }
    }
}