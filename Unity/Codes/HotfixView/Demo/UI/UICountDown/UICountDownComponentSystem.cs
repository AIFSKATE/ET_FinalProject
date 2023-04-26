using System;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace ET
{
    [ObjectSystem]
    public class UICountDownComponentSystemAwakeSystem : AwakeSystem<UICountDownComponent>
    {
        public override void AwakeAsync(UICountDownComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            self.countdown = rc.Get<GameObject>("CountDown").GetComponent<TextMeshProUGUI>();
            self.PanelParent = rc.Get<GameObject>("PanelParent");
            self.Panel = rc.Get<GameObject>("Panel");
            self.YesButton = rc.Get<GameObject>("YesButton").GetComponent<Button>();
            self.uibag = null;
            self.Cell = rc.Get<GameObject>("Cell");
            self.imagelist = (SpriteAtlas)ResourcesComponent.Instance.GetAsset("uisprite.unity3d", "Pixel");
            self.uilist = (SpriteAtlas)ResourcesComponent.Instance.GetAsset("uisprite.unity3d", "uisprite");

            self.BindListener();
        }
    }

    [FriendClass(typeof(UICountDownComponent))]
    public static class UICountDownComponentSystem
    {
        public static void BindListener(this UICountDownComponent self)
        {
            self.YesButton.onClick.AddListener(self.OnYesButton);
        }

        public static async ETTask Prepare(this UICountDownComponent self)
        {
            self.uibag = self.DomainScene().GetComponent<UIComponent>().Get(UIType.UIBag);
            if (self.uibag == null)
            {
                self.uibag = await UIHelper.Show(self.DomainScene(), UIType.UIBag, UILayer.Mid);
                UIHelper.Close(self.DomainScene(), UIType.UIBag).Coroutine();
            }
        }
        public static async void CountDown(this UICountDownComponent self, int time)
        {
            self.Clear();
            self.PanelParent.SetActive(true);
            for (int i = time; i > 0; i--)
            {
                self.countdown.text = i.ToString();
                await TimerComponent.Instance.WaitAsync(1000);
            }
            UIHelper.Close(self.ZoneScene(), UIType.UICountDown).Coroutine();
        }

        public static async void GenerateAndAdd(this UICountDownComponent self)
        {
            await self.Prepare();
            var itemlist = ItemConfigCategory.Instance.GetAll();

            for (int i = 0; i < 11; i++)
            {
                self.uibag.GetComponent<UIBagComponent>().AddItem(i, 10);
                var item = RecyclePoolComponent.Instance.Get(UIType.UICountDown.StringToAB(), UIType.UICountDown, "Cell");
                ReferenceCollector itemrc = item.GetComponent<ReferenceCollector>();
                itemrc.Get<GameObject>("Image").GetComponent<Image>().sprite = self.uilist.GetSprite("GUI_53");
                itemrc.Get<GameObject>("Item").GetComponent<Image>().sprite = self.imagelist.GetSprite(itemlist[i].Name);
                itemrc.Get<GameObject>("Text").GetComponent<TextMeshProUGUI>().text = 10.ToString();
                item.transform.SetParent(self.Panel.transform, false);
            }
        }

        public static void OnYesButton(this UICountDownComponent self)
        {
            self.PanelParent.SetActive(false);
        }

        public static void Clear(this UICountDownComponent self)
        {
            while (self.Panel.transform.childCount > 0)
            {
                RecyclePoolComponent.Instance.Recycle(self.Panel.transform.GetChild(0).gameObject);
            }
        }

    }
}
