using ET.EventType;
using System;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEditor;
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
            self.setBtn = rc.Get<GameObject>("SetBtn").GetComponent<Button>();
            self.exitBtn = rc.Get<GameObject>("ExitBtn").GetComponent<Button>();

            self.BindListener();
        }
    }

    [FriendClass(typeof(UIMainComponent))]
    public static class UIMainComponentSystem
    {
        public static void BindListener(this UIMainComponent self)
        {
            self.startBtn.onClick.AddListener(self.OnStartBtn);
            self.exitBtn.onClick.AddListener(self.OnExitBtn);
            self.setBtn.onClick.AddListener(self.OnSetBtn);
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


            var levelcomponent = currentScenesComponent.Scene.GetComponent<LevelComponent>();
            if (levelcomponent == null)
            {
                levelcomponent = currentScenesComponent.Scene.AddComponent<LevelComponent>();
            }
            var operaComponent = currentScenesComponent.Scene.AddComponent<OperaComponent>();


            //关卡开始
            myunit.GetComponent<MainRoleComponent>().Awake();
            levelcomponent.StandingBy().Coroutine();

            UIHelper.Close(zoneScene, UIType.UIMain).Coroutine();
            UIHelper.Show(zoneScene, UIType.UIHP, UILayer.Mid).Coroutine();
            UIHelper.Create(zoneScene, UIType.UIGame, UILayer.Mid).Coroutine();
        }

        public static void OnExitBtn(this UIMainComponent self)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
            var zoneScene = self.ZoneScene();
            var id = zoneScene.GetComponent<PlayerComponent>().MyId;
            self.ZoneScene().GetComponent<SessionComponent>().Session.Send(new C2M_RemoveUnit() { Id = id });
        }

        public static void OnSetBtn(this UIMainComponent self)
        {
            UIHelper.Show(self.ZoneScene(), UIType.UISetting, UILayer.Mid).Coroutine();
        }

    }
}
