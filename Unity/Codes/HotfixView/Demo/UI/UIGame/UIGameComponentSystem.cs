using System;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace ET
{
    [ObjectSystem]
    public class UIGameComponentAwakeSystem : AwakeSystem<UIGameComponent>
    {
        public override void AwakeAsync(UIGameComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            self.ShowUIDrawBtn = rc.Get<GameObject>("ShowUIDrawBtn").GetComponent<Button>();
            self.ShowUIBagBtn = rc.Get<GameObject>("ShowUIBagBtn").GetComponent<Button>();
            self.consumablePanel = rc.Get<GameObject>("ConsumablePanel").GetComponent<RectTransform>();
            self.skillPanel = rc.Get<GameObject>("SkillPanel").GetComponent<RectTransform>();
            self.skills = new System.Collections.Generic.List<ReferenceCollector>();
            self.numlist = new System.Collections.Generic.List<int>(8);
            self.imagelist = (SpriteAtlas)ResourcesComponent.Instance.GetAsset("uisprite.unity3d", "Pixel");
            var list = (SpriteAtlas)ResourcesComponent.Instance.GetAsset("uisprite.unity3d", "uisprite");
            for (int i = 0; i < 8; i++)
            {
                var item = RecyclePoolComponent.Instance.Get(UIType.UIGame.StringToAB(), UIType.UIGame, "SkillIcon");
                item.transform.SetParent(self.consumablePanel, false);
                item.GetComponent<Image>().sprite = list.GetSprite("GUI_53"); ;
                var trc = item.GetComponent<ReferenceCollector>();
                self.skills.Add(trc);
                self.numlist.Add(0);
                trc.Get<GameObject>("Key").GetComponent<TextMeshProUGUI>().text = (i % 4 + 1) + "";
                trc.Get<GameObject>("Skillicon").GetComponent<Image>().sprite = self.imagelist.GetSprite(ConsumablesConfigCategory.Instance.Get(i).Name);
            }
            self.consumablePanel.GetComponent<Image>().sprite = list.GetSprite("GUI_27");
            self.consumablePanel.GetComponent<Image>().type = Image.Type.Sliced;
            self.skillPanel.GetComponent<Image>().sprite = list.GetSprite("GUI_27");
            self.skillPanel.GetComponent<Image>().type = Image.Type.Sliced;

            self.ShowUIDrawBtn.onClick.AddListener(() => { self.OnShowUIDrawBtn(); });
            self.ShowUIBagBtn.onClick.AddListener(() => { self.OnShowUIBagBtn(); });
        }
    }

    [FriendClass(typeof(UIGameComponent))]
    public static class UIGameComponentSystem
    {
        public static void OnShowUIDrawBtn(this UIGameComponent self)
        {
            UIHelper.Show(self.DomainScene(), UIType.UISkillpanel, UILayer.Mid).Coroutine();
            UIHelper.Close(self.DomainScene(), UIType.UIGame).Coroutine();
        }
        public static void OnShowUIBagBtn(this UIGameComponent self)
        {
            UIHelper.Show(self.DomainScene(), UIType.UIBag, UILayer.Mid).Coroutine();
            UIHelper.Close(self.DomainScene(), UIType.UIGame).Coroutine();
        }

        public static void Generate(this UIGameComponent self, int generateid)
        {
            self.numlist[generateid] += 1;
            self.skills[generateid].Get<GameObject>("Num").GetComponent<TextMeshProUGUI>().text = self.numlist[generateid] + "";
        }

        public static void Refresh(this UIGameComponent self)
        {
            for (int i = 0; i < self.skills.Count; i++)
            {
                self.skills[i].Get<GameObject>("Num").GetComponent<TextMeshProUGUI>().text = self.numlist[i] + "";
            }
        }

        public static async ETTask<bool> Consume(this UIGameComponent self, int id)
        {
            if (self.numlist[id] <= 0)
            {
                await UIHelper.Create(self.DomainScene(), UIType.UITips, UILayer.High);
                var uitips = self.DomainScene().GetComponent<UIComponent>().Get(UIType.UITips);
                uitips.GetComponent<UITipsComponent>().SetContent(1, "You don't have that consumable");
                return false;
            }
            self.numlist[id] -= 1;
            self.skills[id].Get<GameObject>("Num").GetComponent<TextMeshProUGUI>().text = self.numlist[id] + "";
            return true;
        }
    }
}
