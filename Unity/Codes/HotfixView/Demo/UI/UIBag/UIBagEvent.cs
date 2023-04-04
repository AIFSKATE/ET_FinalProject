﻿using System;
using UnityEngine;

namespace ET
{
    [UIEvent(UIType.UIBag)]
    public class UIBagEvent : AUIEvent
    {
        public override async ETTask<UI> OnCreate(UIComponent uiComponent, UILayer uiLayer)
        {
            await ResourcesComponent.Instance.LoadBundleAsync(UIType.UIBag.StringToAB());
            GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset(UIType.UIBag.StringToAB(), UIType.UIBag);
            GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject, UIEventComponent.Instance.UILayers[(int)uiLayer]);
            UI ui = uiComponent.AddChild<UI, string, GameObject>(UIType.UIBag, gameObject);
            ui.AddComponent<UIBagComponent>();
            return ui;
        }

        public override void OnRemove(UIComponent uiComponent)
        {
            ResourcesComponent.Instance.UnloadBundle(UIType.UIBag.StringToAB());
        }

        public override async ETTask<UI> OnShow(UIComponent uiComponent, UILayer uiLayer)
        {
            UI ui = uiComponent.Get(UIType.UIBag);
            var gameObject = ui.GameObject;
            gameObject.SetActive(true);
            gameObject.transform.SetParent(UIEventComponent.Instance.UILayers[(int)uiLayer]);
            await ETTask.CompletedTask;
            return ui;
        }

        public override void OnClose(UIComponent uiComponent)
        {
            UI ui = uiComponent.Get(UIType.UIBag);
            var gameObject = ui.GameObject;
            gameObject.SetActive(false);
        }
    }
}