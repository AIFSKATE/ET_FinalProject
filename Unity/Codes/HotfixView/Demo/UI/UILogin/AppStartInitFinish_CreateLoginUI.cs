

namespace ET
{
    public class AppStartInitFinish_CreateLoginUI : AEvent<EventType.AppStartInitFinish>
    {
        protected override void Run(EventType.AppStartInitFinish args)
        {
            args.ZoneScene.AddComponent<CameraComponent>();
            UIHelper.Create(args.ZoneScene, UIType.UILogin, UILayer.Mid).Coroutine();
        }
    }
}
