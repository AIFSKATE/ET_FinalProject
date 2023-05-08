using System;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
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
            self.playerHP = rc.Get<GameObject>("PlayerHP").GetComponent<Image>();
            self.playerHPBar = rc.Get<GameObject>("PlayerHPBar").GetComponent<Image>();
            self.playerHPBarBg = rc.Get<GameObject>("PlayerHPBarBg").GetComponent<Image>();
            self.htTmp = rc.Get<GameObject>("HPTMP").GetComponent<TextMeshProUGUI>();
            self.attackTmp = rc.Get<GameObject>("AttackTMP").GetComponent<TextMeshProUGUI>();
            self.defenseTmp = rc.Get<GameObject>("DefendTMP").GetComponent<TextMeshProUGUI>();

            self.skills = new System.Collections.Generic.List<ReferenceCollector>();
            self.numlist = new System.Collections.Generic.List<int>(8);
            self.cooltime = new System.Collections.Generic.List<float>(8);
            self.canconsume = new System.Collections.Generic.List<bool>(8);
            self.imagelist = (SpriteAtlas)ResourcesComponent.Instance.GetAsset("uisprite.unity3d", "Pixel");
            self.uilist = (SpriteAtlas)ResourcesComponent.Instance.GetAsset("uisprite.unity3d", "uisprite");

            self.countdown = rc.Get<GameObject>("CountDown").GetComponent<TextMeshProUGUI>();
            self.PanelParent = rc.Get<GameObject>("PanelParent");
            self.TipsParent = rc.Get<GameObject>("Tips");
            self.Panel = rc.Get<GameObject>("Panel");
            self.YesButton = rc.Get<GameObject>("YesButton").GetComponent<Button>();
            self.uibag = null;
            self.Cell = rc.Get<GameObject>("Cell");
            self.Title = rc.Get<GameObject>("Title").GetComponent<Image>();

            self.playerHP.sprite = self.uilist.GetSprite("GUI_20");
            self.playerHPBar.sprite = self.uilist.GetSprite("GUI_19");
            self.playerHPBarBg.sprite = self.uilist.GetSprite("GUI_23");
            self.Title.sprite = self.uilist.GetSprite("GUI_59");
            self.Panel.GetComponent<Image>().sprite = self.uilist.GetSprite("GUI_28");

            self.playerHPBar.type = Image.Type.Sliced;


            for (int i = 0; i < 8; i++)
            {
                var item = RecyclePoolComponent.Instance.Get(UIType.UIGame.StringToAB(), UIType.UIGame, "SkillIcon");
                item.transform.SetParent(self.consumablePanel, false);
                item.GetComponent<Image>().sprite = self.uilist.GetSprite("GUI_53");
                var trc = item.GetComponent<ReferenceCollector>();
                self.skills.Add(trc);
                self.cooltime.Add(ConsumablesConfigCategory.Instance.Get(i).CoolTime);
                self.numlist.Add(i == 4 ? 1 : 10);
                self.canconsume.Add(true);
                trc.Get<GameObject>("Key").GetComponent<TextMeshProUGUI>().text = self.GetKeyName(i);
                trc.Get<GameObject>("Skillicon").GetComponent<Image>().sprite = self.imagelist.GetSprite(ConsumablesConfigCategory.Instance.Get(i).Name);
                trc.Get<GameObject>("Num").GetComponent<TextMeshProUGUI>().text = self.numlist[i] + "";
            }
            self.consumablePanel.GetComponent<Image>().sprite = self.uilist.GetSprite("GUI_27");
            self.consumablePanel.GetComponent<Image>().type = Image.Type.Sliced;
            self.skillPanel.GetComponent<Image>().sprite = self.uilist.GetSprite("GUI_27");
            self.skillPanel.GetComponent<Image>().type = Image.Type.Sliced;

            self.ShowUIDrawBtn.onClick.AddListener(() => { self.OnShowUIDrawBtn(); });
            self.ShowUIBagBtn.onClick.AddListener(() => { self.OnShowUIBagBtn(); });
            self.YesButton.onClick.AddListener(self.OnYesButton);

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

        public static string GetKeyName(this UIGameComponent self, int index)
        {
            string res = string.Empty;
            switch (index)
            {
                case 0: res = "1"; break;
                case 1: res = "2"; break;
                case 2: res = "3"; break;
                case 3: res = "4"; break;
                case 4: res = "Q"; break;
                case 5: res = "W"; break;
                case 6: res = "E"; break;
                case 7: res = "R"; break;
            }
            return res;
        }

        public static void Generate(this UIGameComponent self, int generateid)
        {
            self.numlist[generateid] += 1;
            self.skills[generateid].Get<GameObject>("Num").GetComponent<TextMeshProUGUI>().text = self.numlist[generateid] + "";
        }
        public static int GetNum(this UIGameComponent self, int generateid)
        {
            return self.numlist[generateid];
        }

        public static void Refresh(this UIGameComponent self)
        {
            for (int i = 0; i < self.skills.Count; i++)
            {
                self.skills[i].Get<GameObject>("Num").GetComponent<TextMeshProUGUI>().text = self.numlist[i] + "";
            }
        }

        public static void RefreshHP(this UIGameComponent self, int hp, int maxhp)
        {
            self.htTmp.text = hp + "/" + maxhp;
            self.playerHPBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, self.playerHPBarBg.GetComponent<RectTransform>().rect.width * (float)hp / maxhp);
        }

        public static void RefreshAttack(this UIGameComponent self, int attack)
        {
            self.attackTmp.text = attack.ToString();
        }

        public static void RefreshDefense(this UIGameComponent self, int defense)
        {
            self.defenseTmp.text = defense.ToString();
        }

        public static async ETTask<bool> Consume(this UIGameComponent self, int id)
        {
            if (!self.canconsume[id])
            {
                return false;
            }
            if (self.numlist[id] <= 0)
            {
                await UIHelper.Create(self.DomainScene(), UIType.UITips, UILayer.High);
                var uitips = self.DomainScene().GetComponent<UIComponent>().Get(UIType.UITips);
                uitips.GetComponent<UITipsComponent>().SetContent(1, "You don't have that consumable");
                return false;
            }
            self.numlist[id] -= id == 4 ? 0 : 1;
            self.canconsume[id] = false;
            self.skills[id].Get<GameObject>("Num").GetComponent<TextMeshProUGUI>().text = self.numlist[id] + "";
            self.CoolEffect(id, self.cooltime[id], self.skills[id].Get<GameObject>("Skillicon").GetComponent<Image>()).Coroutine();
            return true;
        }

        public static async ETTask CoolEffect(this UIGameComponent self, int id, float time, Image targetskill)
        {
            time *= 1000;
            float now = 0;
            while (now <= time)
            {
                await TimerComponent.Instance.WaitAsync(100);
                now += 100;
                targetskill.fillAmount = now / time;
            }
            self.canconsume[id] = true;
        }

        public static async void CountDown(this UIGameComponent self, int time)
        {
            self.TipsParent.SetActive(true);
            self.PanelParent.SetActive(true);
            self.ClearCountDown();
            for (int i = time; i > 0; i--)
            {
                self.countdown.text = i.ToString();
                await TimerComponent.Instance.WaitAsync(1000);
            }

            self.TipsParent.SetActive(false);
            self.PanelParent.SetActive(false);
        }

        public static async void GenerateAndAdd(this UIGameComponent self)
        {
            await self.Prepare();
            var itemlist = ItemConfigCategory.Instance.GetAll();

            for (int i = 0; i < 11; i++)
            {
                self.uibag.GetComponent<UIBagComponent>().AddItem(i, 10);
                var item = RecyclePoolComponent.Instance.Get(UIType.UIGame.StringToAB(), UIType.UIGame, "Cell");
                ReferenceCollector itemrc = item.GetComponent<ReferenceCollector>();
                itemrc.Get<GameObject>("Image").GetComponent<Image>().sprite = self.uilist.GetSprite("GUI_53");
                itemrc.Get<GameObject>("Item").GetComponent<Image>().sprite = self.imagelist.GetSprite(itemlist[i].Name);
                itemrc.Get<GameObject>("Text").GetComponent<TextMeshProUGUI>().text = 10.ToString();
                item.transform.SetParent(self.Panel.transform, false);
            }
        }

        public static async ETTask Prepare(this UIGameComponent self)
        {
            self.uibag = self.DomainScene().GetComponent<UIComponent>().Get(UIType.UIBag);
            if (self.uibag == null)
            {
                self.uibag = await UIHelper.Show(self.DomainScene(), UIType.UIBag, UILayer.Mid);
                UIHelper.Close(self.DomainScene(), UIType.UIBag).Coroutine();
            }
        }

        public static void ClearCountDown(this UIGameComponent self)
        {
            while (self.Panel.transform.childCount > 0)
            {
                RecyclePoolComponent.Instance.Recycle(self.Panel.transform.GetChild(0).gameObject);
            }
        }

        public static void OnYesButton(this UIGameComponent self)
        {
            self.PanelParent.SetActive(false);
        }
    }
}
