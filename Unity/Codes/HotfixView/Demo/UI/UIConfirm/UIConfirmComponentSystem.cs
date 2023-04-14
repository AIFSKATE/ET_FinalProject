using System;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace ET
{
    [ObjectSystem]
    public class UIConfirmComponentAwakeSystem : AwakeSystem<UIConfirmComponent>
    {
        public override void AwakeAsync(UIConfirmComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            self.Text = rc.Get<GameObject>("Tips").GetComponent<TextMeshProUGUI>();
            self.yestgl = rc.Get<GameObject>("Yes").GetComponent<Toggle>();
            self.notgl = rc.Get<GameObject>("No").GetComponent<Toggle>();
            self.contentRect = rc.Get<GameObject>("Content").GetComponent<RectTransform>();
            self.imagelist = (SpriteAtlas)ResourcesComponent.Instance.GetAsset("uisprite.unity3d", "Pixel");

            self.yestgl.onValueChanged.AddListener(self.YesAction);
            self.notgl.onValueChanged.AddListener(self.NoAction);
        }
    }

    [FriendClass(typeof(UIConfirmComponent))]
    public static class UIConfirmComponentSystem
    {

        public static async void SetContent(this UIConfirmComponent self, int id, string content, Action yes, Action no)
        {
            await ETTask.CompletedTask;
            self.Text.text = content;
            self.Yes = null;
            self.No = null;

            self.Yes += yes;
            self.No += no;


            await self.Prepare();
            await self.ClearNeedItem();
            var costlist = FuluConfigCategory.Instance.Get(id).Unlockarr;
            int end = costlist.Length;
            for (int i = 0; i < end; i += 2)
            {
                var item = RecyclePoolComponent.Instance.Get(UIType.UIDraw.StringToAB(), UIType.UIDraw, "NeedItem");
                item.GetComponent<Image>().sprite = self.imagelist.GetSprite(ItemConfigCategory.Instance.Get(costlist[i]).Name);
                item.GetComponent<ReferenceCollector>().Get<GameObject>("Text").GetComponent<TextMeshProUGUI>().text =
                    $"Need :{costlist[i + 1]}  Have:{self.uibag.GetComponent<UIBagComponent>().GetNum(costlist[i])}";
                item.transform.SetParent(self.contentRect, false);
            }

        }

        public static async ETTask Prepare(this UIConfirmComponent self)
        {
            self.uibag = self.DomainScene().GetComponent<UIComponent>().Get(UIType.UIBag);
            if (self.uibag == null)
            {
                self.uibag = await UIHelper.Show(self.DomainScene(), UIType.UIBag, UILayer.Mid);
                UIHelper.Close(self.DomainScene(), UIType.UIBag).Coroutine();
            }
        }

        public static async ETTask ClearNeedItem(this UIConfirmComponent self)
        {
            await ETTask.CompletedTask;

            while (self.contentRect.childCount > 0)
            {
                RecyclePoolComponent.Instance.Recycle(self.contentRect.GetChild(0).gameObject);
            }
        }

        public static void YesAction(this UIConfirmComponent self, bool value)
        {
            self.Yes?.Invoke();
            UIHelper.Remove(self.DomainScene(), UIType.UIConfirm).Coroutine();
        }

        public static void NoAction(this UIConfirmComponent self, bool value)
        {
            self.No?.Invoke();
            UIHelper.Remove(self.DomainScene(), UIType.UIConfirm).Coroutine();
        }

    }
}
