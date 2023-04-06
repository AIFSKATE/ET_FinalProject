using ET.EventType;
using UnityEngine;

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
            //加载UI
            UIHelper.Create(args.ZoneScene, UIType.UIGame, UILayer.Mid).Coroutine();
            UIHelper.Create(args.ZoneScene, UIType.UIHP, UILayer.Low).Coroutine();

            //这一块是摄像头的逻辑
            var zoneScene = args.ZoneScene;
            CurrentScenesComponent currentScenesComponent = zoneScene.GetComponent<CurrentScenesComponent>();
            UnitComponent unitComponent = currentScenesComponent.Scene.GetComponent<UnitComponent>();
            var id = zoneScene.GetComponent<PlayerComponent>().MyId;
            var myunit = unitComponent.Get(id);
            zoneScene.RemoveComponent<CameraComponent>();
            zoneScene.AddComponent<RuntimeCameraComponent, Transform>(myunit.GetComponent<GameObjectComponent>().GameObject.transform);

            //这一块是关卡加载通知
            M2C_Standingby m2C_Standingby = await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2M_Standingby()) as M2C_Standingby;
            if (m2C_Standingby.Error == ErrorCode.ERR_Success)
            {
                M2C_Startlevel m2C_Startlevel = await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2M_Startlevel()) as M2C_Startlevel;
                //GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset("Unit.unity3d", "Unit");
                //GameObject prefab = bundleGameObject.Get<GameObject>("Enemy1");
                //GameObject.Instantiate(prefab);
            }

            await ETTask.CompletedTask;
        }
    }
}