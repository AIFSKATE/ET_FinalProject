using System;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [ObjectSystem]
    public class UITipsComponentAwakeSystem : AwakeSystem<UITipsComponent>
    {
        public override void Awake(UITipsComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            self.Text = rc.Get<GameObject>("Tips").GetComponent<TextMeshProUGUI>();
        }
    }

    [FriendClass(typeof(UITipsComponent))]
    public static class UITipsComponentSystem
    {
        public static async void SetContent(this UITipsComponent self, int time, string content, Button selectBtn)
        {
            self.Text.text = content;
            await TimerComponent.Instance.WaitAsync(time * 1000);
            selectBtn.interactable = true;
            UIHelper.Remove(self.ZoneScene(), UIType.UITips).Coroutine();
        }
    }
}
