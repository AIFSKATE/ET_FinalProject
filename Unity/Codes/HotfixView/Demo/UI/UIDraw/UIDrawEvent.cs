using System;
using UnityEngine;

namespace ET
{
    [UIEvent(UIType.UIDraw)]
    public class UIDrawEvent : AUIEvent
    {
        public override async ETTask<UI> OnCreate(UIComponent uiComponent, UILayer uiLayer)
        {
            await uiComponent.Domain.GetComponent<ResourcesLoaderComponent>().LoadAsync(UIType.UIDraw.StringToAB());
            await uiComponent.Domain.GetComponent<ResourcesLoaderComponent>().LoadAsync("material.unity3d");
            GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset(UIType.UIDraw.StringToAB(), UIType.UIDraw);
            GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject, UIEventComponent.Instance.UILayers[(int)uiLayer]);
            UI ui = uiComponent.AddChild<UI, string, GameObject>(UIType.UIDraw, gameObject);
            ui.AddComponent<UIDrawComponent>();
            return ui;
        }

        public override void OnRemove(UIComponent uiComponent)
        {
            ResourcesComponent.Instance.UnloadBundle(UIType.UIDraw.StringToAB());
        }
    }
}