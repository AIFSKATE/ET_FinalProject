using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    [ObjectSystem]
    public class UIEventComponentAwakeSystem : AwakeSystem<UIEventComponent>
    {
        public override void AwakeAsync(UIEventComponent self)
        {
            UIEventComponent.Instance = self;

            GameObject uiRoot = GameObject.Find("/Global/UI");
            ReferenceCollector referenceCollector = uiRoot.GetComponent<ReferenceCollector>();

            self.UILayers.Add((int)UILayer.Hidden, referenceCollector.Get<GameObject>(UILayer.Hidden.ToString()).transform);
            self.UILayers.Add((int)UILayer.Low, referenceCollector.Get<GameObject>(UILayer.Low.ToString()).transform);
            self.UILayers.Add((int)UILayer.Mid, referenceCollector.Get<GameObject>(UILayer.Mid.ToString()).transform);
            self.UILayers.Add((int)UILayer.High, referenceCollector.Get<GameObject>(UILayer.High.ToString()).transform);

            self.StackLayers.Add((int)UILayer.Hidden, new Stack<string>());
            self.StackLayers.Add((int)UILayer.Low, new Stack<string>());
            self.StackLayers.Add((int)UILayer.Mid, new Stack<string>());
            self.StackLayers.Add((int)UILayer.High, new Stack<string>());

            var uiEvents = Game.EventSystem.GetTypes(typeof(UIEventAttribute));
            foreach (Type type in uiEvents)
            {
                object[] attrs = type.GetCustomAttributes(typeof(UIEventAttribute), false);
                if (attrs.Length == 0)
                {
                    continue;
                }

                UIEventAttribute uiEventAttribute = attrs[0] as UIEventAttribute;
                AUIEvent aUIEvent = Activator.CreateInstance(type) as AUIEvent;
                self.UIEvents.Add(uiEventAttribute.UIType, aUIEvent);
            }
        }
    }

    /// <summary>
    /// 管理所有UI GameObject 以及UI事件
    /// </summary>
    [FriendClass(typeof(UIEventComponent))]
    public static class UIEventComponentSystem
    {
        public static async ETTask<UI> OnCreate(this UIEventComponent self, UIComponent uiComponent, string uiType, UILayer uiLayer)
        {
            try
            {
                UI ui = await self.UIEvents[uiType].OnCreate(uiComponent, uiLayer);
                return ui;
            }
            catch (Exception e)
            {
                throw new Exception($"on create ui error: {uiType}", e);
            }
        }

        public static async ETTask<UI> OnShow(this UIEventComponent self, UIComponent uiComponent, string uiType, UILayer uiLayer)
        {
            try
            {
                UI ui = await self.UIEvents[uiType].OnShow(uiComponent, uiLayer);
                return ui;
            }
            catch (Exception e)
            {
                throw new Exception($"on show ui error: {uiType}", e);
            }
        }

        public static void OnRemove(this UIEventComponent self, UIComponent uiComponent, string uiType)
        {
            try
            {
                self.UIEvents[uiType].OnRemove(uiComponent);
            }
            catch (Exception e)
            {
                throw new Exception($"on remove ui error: {uiType}", e);
            }

        }

        public static void OnClose(this UIEventComponent self, UIComponent uiComponent, string uiType)
        {
            try
            {
                self.UIEvents[uiType].OnClose(uiComponent);
            }
            catch (Exception e)
            {
                throw new Exception($"on close ui error: {uiType}", e);
            }

        }
    }
}