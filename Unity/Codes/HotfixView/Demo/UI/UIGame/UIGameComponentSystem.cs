using System;
using System.Net;

using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [ObjectSystem]
    public class UIGameComponentAwakeSystem : AwakeSystem<UIGameComponent>
    {
        public override void Awake(UIGameComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            self.ShowUIDrawBtn = rc.Get<GameObject>("ShowUIDrawBtn").GetComponent<Button>();
            self.ShowUIBagBtn = rc.Get<GameObject>("ShowUIBagBtn").GetComponent<Button>();
            self.ShowUIDrawBtn.onClick.AddListener(() => { self.OnShowUIDrawBtn(); });
            self.ShowUIBagBtn.onClick.AddListener(() => { self.OnShowUIBagBtn(); });
        }
    }

    [FriendClass(typeof(UIGameComponent))]
    public static class UIGameComponentSystem
    {
        public static void OnShowUIDrawBtn(this UIGameComponent self)
        {
            UIHelper.Show(self.DomainScene(), UIType.UISkillpanel, UILayer.Mid).Coroutine();
            UIHelper.Close(self.DomainScene(), UIType.UIGame).Coroutine();
        }
        public static void OnShowUIBagBtn(this UIGameComponent self)
        {
            UIHelper.Show(self.DomainScene(), UIType.UIBag, UILayer.Mid).Coroutine();
            UIHelper.Close(self.DomainScene(), UIType.UIGame).Coroutine();
        }
    }
}
