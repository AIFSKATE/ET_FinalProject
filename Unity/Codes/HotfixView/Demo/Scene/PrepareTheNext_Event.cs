using System.Collections.Generic;

namespace ET
{
    public class PrepareTheNext_Event : AEvent<EventType.PrepareTheNext>
    {
        protected override void Run(EventType.PrepareTheNext args)
        {
            RunAsync(args).Coroutine();
        }

        protected async ETTask RunAsync(EventType.PrepareTheNext args)
        {
            //UI
            var zonescene = args.ZoneScene;
            await ETTask.CompletedTask;
            //await UIHelper.Create(zonescene, UIType.UICountDown, UILayer.High);
            var uigame = zonescene.GetComponent<UIComponent>().Get(UIType.UIGame).GetComponent<UIGameComponent>();
            uigame.CountDown(args.time);
            uigame.GenerateAndAdd();
        }
    }
}