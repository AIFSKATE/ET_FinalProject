using System;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace ET
{
    [ObjectSystem]
    public class UIMainComponentAwakeSystem : AwakeSystem<UIMainComponent>
    {
        public override void AwakeAsync(UIMainComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            self.startBtn = rc.Get<GameObject>("StartBtn").GetComponent<Button>();

            self.BindListener();
        }
    }

    [FriendClass(typeof(UIMainComponent))]
    public static class UIMainComponentSystem
    {
        public static void BindListener(this UIMainComponent self)
        {
            self.startBtn.onClick.AddListener(self.OnStartBtn);
        }

        public static void OnStartBtn(this UIMainComponent self)
        {
            //这一块是摄像头的逻辑
            var zoneScene = self.ZoneScene();
            CurrentScenesComponent currentScenesComponent = zoneScene.GetComponent<CurrentScenesComponent>();
            UnitComponent unitComponent = currentScenesComponent.Scene.GetComponent<UnitComponent>();
            var id = zoneScene.GetComponent<PlayerComponent>().MyId;
            var myunit = unitComponent.Get(id);
            var cameraComponent = zoneScene.GetComponent<CameraComponent>();
            cameraComponent.RemoveComponent<CameraUpdateComponent>();
            zoneScene.AddComponent<RuntimeCameraComponent, Transform>(myunit.GetComponent<GameObjectComponent>().GameObject.transform);


            var levelcomponent = currentScenesComponent.Scene.AddComponent<LevelComponent>();
            var operaComponent = currentScenesComponent.Scene.AddComponent<OperaComponent>();

            //关卡开始
            levelcomponent.StandingBy().Coroutine();

            UIHelper.Close(zoneScene, UIType.UIMain).Coroutine();
            UIHelper.Create(zoneScene, UIType.UIGame, UILayer.Mid).Coroutine();
        }

    }
}
