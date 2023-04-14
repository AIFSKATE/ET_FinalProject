using System;
using UnityEngine;

namespace ET
{
    [UIEvent(UIType.UIDraw)]
    public class UIDrawEvent : AUIEvent
    {
        public override async ETTask<UI> OnCreate(UIComponent uiComponent, UILayer uiLayer)
        {
            await ResourcesComponent.Instance.LoadBundleAsync(UIType.UIDraw.StringToAB());
            await uiComponent.Domain.GetComponent<ResourcesLoaderComponent>().LoadAsync("material.unity3d");
            await ResourcesComponent.Instance.LoadBundleAsync("uisprite.unity3d");
            GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset(UIType.UIDraw.StringToAB(), UIType.UIDraw);
            GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject, UIEventComponent.Instance.UILayers[(int)uiLayer]);
            UI ui = uiComponent.AddChild<UI, string, GameObject>(UIType.UIDraw, gameObject);
            ui.AddComponent<UIDrawComponent>();
            return ui;
        }

        public override void OnRemove(UIComponent uiComponent)
        {
            ResourcesComponent.Instance.UnloadBundle(UIType.UIDraw.StringToAB());
            ResourcesComponent.Instance.UnloadBundle("uisprite.unity3d");
            ResourcesComponent.Instance.UnloadBundle("material.unity3d");
        }

        public override async ETTask<UI> OnShow(UIComponent uiComponent, UILayer uiLayer)
        {
            UI ui = uiComponent.Get(UIType.UIDraw);
            var gameObject = ui.GameObject;
            gameObject.SetActive(true);
            gameObject.transform.SetParent(UIEventComponent.Instance.UILayers[(int)uiLayer]);

            ui.GetComponent<UIDrawComponent>().Clear();
            await ETTask.CompletedTask;
            return ui;
        }

        public override void OnClose(UIComponent uiComponent)
        {
            UI ui = uiComponent.Get(UIType.UIDraw);
            var gameObject = ui.GameObject;
            gameObject.SetActive(false);
        }
    }
}