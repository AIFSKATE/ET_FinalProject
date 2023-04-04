using System;
using UnityEngine;

namespace ET
{
	[UIEvent(UIType.UIHelp)]
    public class UIHelpEvent: AUIEvent
    {
        public override async ETTask<UI> OnCreate(UIComponent uiComponent, UILayer uiLayer)
        {
	        try
	        {
		        await ResourcesComponent.Instance.LoadBundleAsync(UIType.UIHelp.StringToAB());
		        GameObject bundleGameObject = (GameObject) ResourcesComponent.Instance.GetAsset(UIType.UIHelp.StringToAB(), UIType.UIHelp);
		        GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject, UIEventComponent.Instance.UILayers[(int)uiLayer]);
		        UI ui = uiComponent.AddChild<UI, string, GameObject>(UIType.UIHelp, gameObject);

				ui.AddComponent<UIHelpComponent>();
				return ui;
	        }
	        catch (Exception e)
	        {
				Log.Error(e);
		        return null;
	        }
		}

        public override void OnRemove(UIComponent uiComponent)
        {
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
            UI ui = uiComponent.Get(UIType.UIHelp);
            var gameObject = ui.GameObject;
            gameObject.SetActive(false);
        }
    }
}