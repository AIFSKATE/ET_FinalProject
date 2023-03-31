using System;
using UnityEngine;

namespace ET
{
    [UIEvent(UIType.UITips)]
    public class UITipsEvent : AUIEvent
    {
        public override async ETTask<UI> OnCreate(UIComponent uiComponent, UILayer uiLayer)
        {
            await ResourcesComponent.Instance.LoadBundleAsync(UIType.UITips.StringToAB());
            GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset(UIType.UITips.StringToAB(), UIType.UITips);
            GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject, UIEventComponent.Instance.UILayers[(int)uiLayer]);
            UI ui = uiComponent.AddChild<UI, string, GameObject>(UIType.UITips, gameObject);
            ui.AddComponent<UITipsComponent>();
            return ui;
        }

        public override void OnRemove(UIComponent uiComponent)
        {
            ResourcesComponent.Instance.UnloadBundle(UIType.UITips.StringToAB());
        }
    }
}