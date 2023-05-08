

namespace ET
{
    public class LoginFinish_CreateLobbyUI : AEvent<EventType.LoginFinish>
    {
        protected override async void Run(EventType.LoginFinish args)
        {
            await UIHelper.Create(args.ZoneScene, UIType.UIHP, UILayer.Low);
            UIHelper.Close(args.ZoneScene, UIType.UIHP).Coroutine();
            await UIHelper.Create(args.ZoneScene, UIType.UIGame, UILayer.Mid);
            UIHelper.Close(args.ZoneScene, UIType.UIGame).Coroutine();
            await UIHelper.Create(args.ZoneScene, UIType.UITips, UILayer.Mid);
            UIHelper.Close(args.ZoneScene, UIType.UITips).Coroutine();
            UIHelper.Create(args.ZoneScene, UIType.UILobby, UILayer.Mid).Coroutine();
        }
    }
}
