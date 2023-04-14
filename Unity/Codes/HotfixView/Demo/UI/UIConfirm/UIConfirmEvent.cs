using System;
using UnityEngine;

namespace ET
{
    [UIEvent(UIType.UIConfirm)]
    public class UIConfirmEvent : AUIEvent
    {
        public override async ETTask<UI> OnCreate(UIComponent uiComponent, UILayer uiLayer)
        {
            await ResourcesComponent.Instance.LoadBundleAsync(UIType.UIConfirm.StringToAB());
            await ResourcesComponent.Instance.LoadBundleAsync(UIType.UIBag.StringToAB());
            await ResourcesComponent.Instance.LoadBundleAsync(UIType.UIDraw.StringToAB());
            await ResourcesComponent.Instance.LoadBundleAsync("uisprite.unity3d");
            GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset(UIType.UIConfirm.StringToAB(), UIType.UIConfirm);
            GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject, UIEventComponent.Instance.UILayers[(int)uiLayer]);
            UI ui = uiComponent.AddChild<UI, string, GameObject>(UIType.UIConfirm, gameObject);
            ui.AddComponent<UIConfirmComponent>();
            return ui;
        }

        public override void OnRemove(UIComponent uiComponent)
        {
            ResourcesComponent.Instance.UnloadBundle(UIType.UIConfirm.StringToAB());
            ResourcesComponent.Instance.UnloadBundle(UIType.UIBag.StringToAB());
            ResourcesComponent.Instance.UnloadBundle(UIType.UIDraw.StringToAB());
            ResourcesComponent.Instance.UnloadBundle("uisprite.unity3d");
        }

        public override async ETTask<UI> OnShow(UIComponent uiComponent, UILayer uiLayer)
        {
            UI ui = uiComponent.Get(UIType.UIConfirm);
            var gameObject = ui.GameObject;
            gameObject.SetActive(true);
            gameObject.transform.SetParent(UIEventComponent.Instance.UILayers[(int)uiLayer]);
            await ETTask.CompletedTask;
            return ui;
        }

        public override void OnClose(UIComponent uiComponent)
        {
            UI ui = uiComponent.Get(UIType.UIConfirm);
            var gameObject = ui.GameObject;
            gameObject.SetActive(false);
        }
    }
}