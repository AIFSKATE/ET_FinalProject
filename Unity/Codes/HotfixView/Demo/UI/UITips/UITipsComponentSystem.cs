using System;
using System.Net;

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

        }
    }

    [FriendClass(typeof(UITipsComponent))]
    public static class UITipsComponentSystem
    {
        public static void OnShowUIDrawBtn(this UITipsComponent self)
        {
            
        }
    }
}
