

namespace ET
{
    public class AppStartInitFinish_CreateLoginUI : AEvent<EventType.AppStartInitFinish>
    {
        protected override void Run(EventType.AppStartInitFinish args)
        {
            var cameraComponent = args.ZoneScene.AddComponent<CameraComponent>();
            cameraComponent.AddComponent<CameraUpdateComponent>();
            UIHelper.Create(args.ZoneScene, UIType.UILogin, UILayer.Mid).Coroutine();
        }
    }
}
