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
            UIHelper.Create(args.ZoneScene, UIType.UIMain, UILayer.Mid).Coroutine();

            //迁移到MainUI
            ////这一块是摄像头的逻辑
            //var zoneScene = args.ZoneScene;
            //CurrentScenesComponent currentScenesComponent = zoneScene.GetComponent<CurrentScenesComponent>();
            //UnitComponent unitComponent = currentScenesComponent.Scene.GetComponent<UnitComponent>();
            //var id = zoneScene.GetComponent<PlayerComponent>().MyId;
            //var myunit = unitComponent.Get(id);
            //var cameraComponent = zoneScene.GetComponent<CameraComponent>();
            //cameraComponent.RemoveComponent<CameraUpdateComponent>();
            //zoneScene.AddComponent<RuntimeCameraComponent, Transform>(myunit.GetComponent<GameObjectComponent>().GameObject.transform);

            ////添加关卡加载组件
            //var levelcomponent = currentScenesComponent.Scene.AddComponent<LevelComponent>();
            //var operaComponent = currentScenesComponent.Scene.AddComponent<OperaComponent>();
            ////关卡开始
            //levelcomponent.StandingBy().Coroutine();

            await ETTask.CompletedTask;
        }
    }
}