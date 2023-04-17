

namespace ET
{
    public class LoginFinish_CreateLobbyUI : AEvent<EventType.LoginFinish>
    {
        protected override async void Run(EventType.LoginFinish args)
        {
            UIHelper.Create(args.ZoneScene, UIType.UILobby, UILayer.Mid).Coroutine();
            UIHelper.Create(args.ZoneScene, UIType.UIHP, UILayer.Low).Coroutine();
            await UIHelper.Create(args.ZoneScene, UIType.UIGame, UILayer.Mid);
            UIHelper.Close(args.ZoneScene, UIType.UIGame).Coroutine();
            await UIHelper.Create(args.ZoneScene, UIType.UITips, UILayer.Mid);
            UIHelper.Close(args.ZoneScene, UIType.UITips).Coroutine();
        }
    }
}
