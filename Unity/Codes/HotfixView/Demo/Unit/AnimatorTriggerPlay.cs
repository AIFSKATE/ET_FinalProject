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
    public class AnimatorTriggerPlay : AEvent<EventType.AnimatorTrigger>
    {
        protected override void Run(AnimatorTrigger args)
        {
            RunAsync(args).Coroutine();
        }
        protected async ETTask RunAsync(AnimatorTrigger args)
        {
            await ETTask.CompletedTask;
            Unit unit = args.Unit;

            unit.GetComponent<AnimatorComponent>()?.SetTrigger(args.trigger);
        }
    }
}
