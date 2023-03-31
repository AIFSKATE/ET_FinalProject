using System.Collections.Generic;
using System.Xml.Linq;

namespace ET
{
	/// <summary>
	/// 管理Scene上的UI
	/// </summary>
	[FriendClass(typeof(UIComponent))]
	public static class UIComponentSystem
	{
        public static async ETTask<UI> Create(this UIComponent self, string uiType, UILayer uiLayer)
        {
            UI ui = await UIEventComponent.Instance.OnCreate(self, uiType, uiLayer);
            self.UIs.Add(uiType, ui);
            return ui;
        }
        public static void Close(this UIComponent self, string uiType)
        {
            if (!self.UIs.TryGetValue(uiType, out UI ui))
            {
                return;
            }

			//ui.GetComponent<GameObjectComponent>().GameObject.SetActive(false);
            //UIEventComponent.Instance.OnRemove(self, uiType);
        }

        public static void Remove(this UIComponent self, string uiType)
		{
			if (!self.UIs.TryGetValue(uiType, out UI ui))
			{
				return;
			}
			
			UIEventComponent.Instance.OnRemove(self, uiType);
			
			self.UIs.Remove(uiType);
			ui.Dispose();
		}

		public static UI Get(this UIComponent self, string name)
		{
			UI ui = null;
			self.UIs.TryGetValue(name, out ui);
			return ui;
		}
	}
}