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
            self.testBtn = rc.Get<GameObject>("TestBtn");
            self.testBtn.GetComponent<Button>().onClick.AddListener(() => { self.OnTestBtn(); });
            //self.loginBtn = rc.Get<GameObject>("LoginBtn");

            //self.loginBtn.GetComponent<Button>().onClick.AddListener(() => { self.OnLogin(); });
            //self.account = rc.Get<GameObject>("Account");
            //self.password = rc.Get<GameObject>("Password");
        }
    }

    [FriendClass(typeof(UIGameComponent))]
    public static class UIGameComponentSystem
    {
        public static void OnTestBtn(this UIGameComponent self)
        {
            Log.Warning("测试通过");
        }
    }
}
