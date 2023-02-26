using ET.EventType;

namespace ET
{
    public class EnterMapFinish_Prepare : AEvent<EventType.EnterMapFinish>
    {
        protected override void Run(EventType.EnterMapFinish args)
        {
            RunAsync(args).Coroutine();
        }

        private async ETTask RunAsync(EventType.EnterMapFinish args)
        {
            var zoneScene=args.ZoneScene;
            CurrentScenesComponent currentScenesComponent = zoneScene.GetComponent<CurrentScenesComponent>();
            UnitComponent unitComponent = currentScenesComponent.Scene.GetComponent<UnitComponent>();
            var id = zoneScene.GetComponent<PlayerComponent>().MyId;
            var myunit = unitComponent.Get(id);
            CameraComponent.Instance.SetTarget(myunit.GetComponent<GameObjectComponent>().GameObject.transform);
            await ETTask.CompletedTask;
        }
    }
}