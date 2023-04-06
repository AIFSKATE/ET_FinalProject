using System;
using System.Net;

using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [ObjectSystem]
    public class UIHPComponentAwakeSystem : AwakeSystem<UIHPComponent>
    {
        public override void Awake(UIHPComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            self.hpimage = rc.Get<GameObject>("HP");
            self.panel = rc.Get<GameObject>("Panel").GetComponent<RectTransform>();
            self.dichp = new System.Collections.Generic.Dictionary<Unit, RectTransform>();
        }
    }

    [FriendClass(typeof(UIHPComponent))]
    public static class UIHPComponentSystem
    {
        public static void SetHP(this UIHPComponent self, GameObject gameObject, Unit unit)
        {
            Vector3 screenpositon = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(self.panel, screenpositon, null, out var pos);
            pos.y += 100;
            self.dichp[unit].anchoredPosition = pos;
        }

        public static void InitHP(this UIHPComponent self, Unit unit)
        {
            var t = GameObject.Instantiate(self.hpimage, self.panel);
            self.dichp.Add(unit, t.GetComponent<RectTransform>());
        }
    }
}
