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
        public static void SetHP(this UIHPComponent self, GameObject gameObject, Unit unit, int hp, int maxhp, int width)
        {
            Vector3 screenpositon = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(self.panel, screenpositon, null, out var pos);
            pos.y += 100;
            pos.x -= width / 2;
            self.dichp[unit].anchoredPosition = pos;
            self.dichp[unit].SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hp / (float)maxhp * width);
        }

        public static void InitHP(this UIHPComponent self, Unit unit, float width, float height)
        {
            //var t = GameObject.Instantiate(self.hpimage, self.panel);
            var t = RecyclePoolComponent.Instance.Get(UIType.UIHP.StringToAB(), UIType.UIHP, "HP"); ;
            t.transform.SetParent(self.panel, false);

            t.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            t.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            self.dichp.Add(unit, t.GetComponent<RectTransform>());
        }

        public static void Recycle(this UIHPComponent self, GameObject gameObject, Unit unit)
        {
            RecyclePoolComponent.Instance.Recycle(self.dichp[unit].gameObject);
            self.dichp.Remove(unit);
        }
    }
}
