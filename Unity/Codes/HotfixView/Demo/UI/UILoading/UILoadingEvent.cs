using System;
using UnityEngine;

namespace ET
{
	[UIEvent(UIType.UILoading)]
    public class UILoadingEvent: AUIEvent
    {
        public override async ETTask<UI> OnCreate(UIComponent uiComponent, UILayer uiLayer)
        {
	        try
	        {
		        await ETTask.CompletedTask;
				GameObject bundleGameObject = ((GameObject)Resources.Load("KV")).Get<GameObject>(UIType.UILoading);
				GameObject go = UnityEngine.Object.Instantiate(bundleGameObject);
				go.layer = LayerMask.NameToLayer(LayerNames.UI);
				UI ui = uiComponent.AddChild<UI, string, GameObject>(UIType.UILoading, go);

				ui.AddComponent<UILoadingComponent>();
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
            UI ui = uiComponent.Get(UIType.UILoading);
            var gameObject = ui.GameObject;
            gameObject.SetActive(true);
            gameObject.transform.SetParent(UIEventComponent.Instance.UILayers[(int)uiLayer]);
            await ETTask.CompletedTask;
            return ui;
        }

        public override void OnClose(UIComponent uiComponent)
        {
            UI ui = uiComponent.Get(UIType.UILoading);
            var gameObject = ui.GameObject;
            gameObject.SetActive(false);
        }
    }
}