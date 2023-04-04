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
            if (self.UIs.ContainsKey(uiType))
            {
                return await UIEventComponent.Instance.OnShow(self, uiType, uiLayer);
            }
            UI ui = await UIEventComponent.Instance.OnCreate(self, uiType, uiLayer);
            self.UIs.Add(uiType, ui);
            return ui;
        }

        public static async ETTask<UI> Show(this UIComponent self, string uiType, UILayer uiLayer)
        {
            if (self.UIs.ContainsKey(uiType))
            {
                return await UIEventComponent.Instance.OnShow(self, uiType, uiLayer);
            }
            return await self.Create(uiType, uiLayer);
        }

        public static void Close(this UIComponent self, string uiType)
        {
            if (!self.UIs.ContainsKey(uiType))
            {
                return;
            }

            UIEventComponent.Instance.OnClose(self, uiType);
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