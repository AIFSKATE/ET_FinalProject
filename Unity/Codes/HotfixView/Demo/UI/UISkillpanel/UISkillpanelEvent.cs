using System;
using UnityEngine;

namespace ET
{
    [UIEvent(UIType.UISkillpanel)]
    public class UISkillpanelEvent : AUIEvent
    {
        public override async ETTask<UI> OnCreate(UIComponent uiComponent, UILayer uiLayer)
        {
            await ResourcesComponent.Instance.LoadBundleAsync(UIType.UISkillpanel.StringToAB());
            GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset(UIType.UISkillpanel.StringToAB(), UIType.UISkillpanel);
            GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject, UIEventComponent.Instance.UILayers[(int)uiLayer]);
            UI ui = uiComponent.AddChild<UI, string, GameObject>(UIType.UISkillpanel, gameObject);
            ui.AddComponent<UISkillpanelComponent>();
            return ui;
        }

        public override void OnRemove(UIComponent uiComponent)
        {
            ResourcesComponent.Instance.UnloadBundle(UIType.UISkillpanel.StringToAB());
        }

        public override async ETTask<UI> OnShow(UIComponent uiComponent, UILayer uiLayer)
        {
            UI ui = uiComponent.Get(UIType.UISkillpanel);
            var gameObject = ui.GameObject;
            gameObject.SetActive(true);
            gameObject.transform.SetParent(UIEventComponent.Instance.UILayers[(int)uiLayer]);
            await ETTask.CompletedTask;
            return ui;
        }

        public override void OnClose(UIComponent uiComponent)
        {
            UI ui = uiComponent.Get(UIType.UISkillpanel);
            var gameObject = ui.GameObject;
            gameObject.SetActive(false);
        }
    }
}