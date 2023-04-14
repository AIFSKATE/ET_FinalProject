namespace ET
{
    public class EndLevel_Finish : AEvent<EventType.EndLevel>
    {
        protected override void Run(EventType.EndLevel args)
        {
            RunAsync(args).Coroutine();
        }

        protected async ETTask RunAsync(EventType.EndLevel args)
        {
            //UI
            var zonescene = args.ZoneScene;
            await UIHelper.Create(zonescene, UIType.UITips, UILayer.High);
            var uitips = zonescene.GetComponent<UIComponent>().Get(UIType.UITips);
            for (int i = args.time; i > 0; i--)
            {
                uitips.GetComponent<UITipsComponent>().SetContent("It will end in " + i + " s");
                await TimerComponent.Instance.WaitAsync(1000);
            }
            UIHelper.Remove(zonescene, UIType.UITips).Coroutine();
            UIHelper.Remove(zonescene, UIType.UIGame).Coroutine();
            UIHelper.Show(zonescene, UIType.UIMain, UILayer.Mid).Coroutine();

            //通知后端
            //G2C_ExitMap g2CExitMap = await zonescene.GetComponent<SessionComponent>().Session.Call(new C2G_ExitMap()) as G2C_ExitMap;

            zonescene.CurrentScene().RemoveComponent<LevelComponent>();
            zonescene.CurrentScene().RemoveComponent<OperaComponent>();

            //Camera
            zonescene.RemoveComponent<RuntimeCameraComponent>();
            var cameraComponent = zonescene.GetComponent<CameraComponent>();
            cameraComponent.AddComponent<CameraUpdateComponent>();
        }
    }
}