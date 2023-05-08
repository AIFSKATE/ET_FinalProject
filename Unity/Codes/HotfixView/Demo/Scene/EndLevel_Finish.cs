using System.Collections.Generic;

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

            //移除所有敌人
            zonescene.GetComponent<SessionComponent>().Session.Send(new C2M_RemoveAllEnemyUnit());
            var id = zonescene.GetComponent<PlayerComponent>().MyId;
            zonescene.GetComponent<SessionComponent>().Session.Send(new C2M_RemoveUnit() { Id = id });

            UIHelper.Remove(zonescene, UIType.UITips).Coroutine();
            UIHelper.Close(zonescene, UIType.UIGame).Coroutine();
            UIHelper.Close(zonescene, UIType.UIHP).Coroutine();
            UIHelper.Close(zonescene, UIType.UIBag).Coroutine();
            UIHelper.Close(zonescene, UIType.UISkillpanel).Coroutine();
            UIHelper.Close(zonescene, UIType.UIDraw).Coroutine();
            UIHelper.Show(zonescene, UIType.UILobby, UILayer.Mid).Coroutine();

            zonescene.CurrentScene().RemoveComponent<LevelComponent>();
            zonescene.CurrentScene().RemoveComponent<OperaComponent>();

            //Camera
            zonescene.RemoveComponent<RuntimeCameraComponent>();
            var cameraComponent = zonescene.GetComponent<CameraComponent>();
            cameraComponent.AddComponent<CameraUpdateComponent>();
        }
    }
}