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
            self.ShowUIDrawBtn = rc.Get<GameObject>("ShowUIDrawBtn");
            self.ShowUIDrawBtn.GetComponent<Button>().onClick.AddListener(() => { self.OnShowUIDrawBtn(); });
            //self.loginBtn = rc.Get<GameObject>("LoginBtn");

            //self.loginBtn.GetComponent<Button>().onClick.AddListener(() => { self.OnLogin(); });
            //self.account = rc.Get<GameObject>("Account");
            //self.password = rc.Get<GameObject>("Password");
        }
    }

    [FriendClass(typeof(UIGameComponent))]
    public static class UIGameComponentSystem
    {
        public static void OnShowUIDrawBtn(this UIGameComponent self)
        {
            UIHelper.Create(self.DomainScene(), UIType.UIDraw, UILayer.Mid).Coroutine();
        }
    }
}
