using System;
using UnityEngine;

namespace ET
{
    [UIEvent(UIType.UIGame)]
    public class UIGameEvent : AUIEvent
    {
        public override async ETTask<UI> OnCreate(UIComponent uiComponent, UILayer uiLayer)
        {
            await ResourcesComponent.Instance.LoadBundleAsync(UIType.UIGame.StringToAB());
            GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset(UIType.UIGame.StringToAB(), UIType.UIGame);
            GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject, UIEventComponent.Instance.UILayers[(int)uiLayer]);
            UI ui = uiComponent.AddChild<UI, string, GameObject>(UIType.UIGame, gameObject);
            ui.AddComponent<UIGameComponent>();
            return ui;
        }

        public override void OnRemove(UIComponent uiComponent)
        {
            ResourcesComponent.Instance.UnloadBundle(UIType.UIGame.StringToAB());
        }

        public override async ETTask<UI> OnShow(UIComponent uiComponent, UILayer uiLayer)
        {
            UI ui = uiComponent.Get(UIType.UIGame);
            var gameObject = ui.GameObject;
            gameObject.SetActive(true);
            gameObject.transform.SetParent(UIEventComponent.Instance.UILayers[(int)uiLayer]);
            await ETTask.CompletedTask;
            return ui;
        }

        public override void OnClose(UIComponent uiComponent)
        {
            UI ui = uiComponent.Get(UIType.UIGame);
            var gameObject = ui.GameObject;
            gameObject.SetActive(false);
        }
    }
}