using System;
using UnityEngine;

namespace ET
{
    [UIEvent(UIType.UIHP)]
    public class UIHPEvent : AUIEvent
    {
        public override async ETTask<UI> OnCreate(UIComponent uiComponent, UILayer uiLayer)
        {
            await ResourcesComponent.Instance.LoadBundleAsync(UIType.UIHP.StringToAB());
            GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset(UIType.UIHP.StringToAB(), UIType.UIHP);
            //GameObject bundleGameObject = await AddressablesMgrComponent.Instance.LoadAssetAsync<GameObject>(UIType.UILogin);
            GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject, UIEventComponent.Instance.UILayers[(int)uiLayer]);
            UI ui = uiComponent.AddChild<UI, string, GameObject>(UIType.UIHP, gameObject);
            ui.AddComponent<UIHPComponent>();
            return ui;
        }

        public override void OnRemove(UIComponent uiComponent)
        {
            ResourcesComponent.Instance.UnloadBundle(UIType.UIHP.StringToAB());
            //AddressablesMgrComponent.Instance.Release<GameObject>(UIType.UILogin);
        }

        public override async ETTask<UI> OnShow(UIComponent uiComponent, UILayer uiLayer)
        {
            UI ui = uiComponent.Get(UIType.UIHP);
            var gameObject = ui.GameObject;
            gameObject.SetActive(true);
            gameObject.transform.SetParent(UIEventComponent.Instance.UILayers[(int)uiLayer]);
            await ETTask.CompletedTask;
            return ui;
        }

        public override void OnClose(UIComponent uiComponent)
        {
            UI ui = uiComponent.Get(UIType.UIHP);
            var gameObject = ui.GameObject;
            gameObject.SetActive(false);
        }
    }
}