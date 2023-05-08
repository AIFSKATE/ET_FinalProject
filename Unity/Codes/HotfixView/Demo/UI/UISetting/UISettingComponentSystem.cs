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
    public class UISettingComponentAwakeSystem : AwakeSystem<UISettingComponent>
    {
        public override void AwakeAsync(UISettingComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            self.exitpanel = rc.Get<GameObject>("Panel").GetComponent<Button>();
            self.bk = rc.Get<GameObject>("Bk").GetComponent<Image>();
            self.handle = rc.Get<GameObject>("Handle").GetComponent<Image>();
            self.uilist = (SpriteAtlas)ResourcesComponent.Instance.GetAsset("uisprite.unity3d", "uisprite");

            self.BindListener();
        }
    }

    [FriendClass(typeof(UISettingComponent))]
    public static class UISettingComponentSystem
    {
        public static void BindListener(this UISettingComponent self)
        {
            self.handle.sprite = self.uilist.GetSprite("GUI_7");
            self.bk.sprite = self.uilist.GetSprite("GUI_28");

            self.exitpanel.onClick.AddListener(self.OnExitButton);
        }

        public static void OnExitButton(this UISettingComponent self)
        {
            UIHelper.Close(self.ZoneScene(), UIType.UISetting).Coroutine();
        }

    }
}
