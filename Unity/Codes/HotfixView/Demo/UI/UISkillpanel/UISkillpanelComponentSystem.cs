using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace ET
{
    [ObjectSystem]
    public class UISkillpanelComponentAwakeSystem : AwakeSystem<UISkillpanelComponent>
    {
        public override void AwakeAsync(UISkillpanelComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            self.chosenId = -1;
            self.selectBtn = rc.Get<GameObject>("SelectBtn").GetComponent<Button>();
            self.backBtn = rc.Get<GameObject>("BackBtn").GetComponent<Button>();
            self.skillTglGroup = rc.Get<GameObject>("TglGroup").GetComponent<ToggleGroup>();
            self.skillTgl_1 = rc.Get<GameObject>("Skill1").GetComponent<Toggle>();
            self.skillTgl_2 = rc.Get<GameObject>("Skill2").GetComponent<Toggle>();
            self.skillTgl_3 = rc.Get<GameObject>("Skill3").GetComponent<Toggle>();
            self.skillTgl_4 = rc.Get<GameObject>("Skill4").GetComponent<Toggle>();
            self.skillTgl_5 = rc.Get<GameObject>("Skill5").GetComponent<Toggle>();
            self.skillTgl_6 = rc.Get<GameObject>("Skill6").GetComponent<Toggle>();
            self.skillTgl_7 = rc.Get<GameObject>("Skill7").GetComponent<Toggle>();

            self.toggles = new List<Toggle>();
            self.toggles.Add(self.skillTgl_1);
            self.toggles.Add(self.skillTgl_2);
            self.toggles.Add(self.skillTgl_3);
            self.toggles.Add(self.skillTgl_4);
            self.toggles.Add(self.skillTgl_5);
            self.toggles.Add(self.skillTgl_6);
            self.toggles.Add(self.skillTgl_7);

            self.already = new HashSet<int>();

            self.InitSkill().Coroutine();

            self.BindListener().Coroutine();
        }
    }



    [FriendClass(typeof(UISkillpanelComponent))]
    public static class UISkillpanelComponentSystem
    {
        public static async ETTask BindListener(this UISkillpanelComponent self)
        {
            await self.DomainScene().GetComponent<ResourcesLoaderComponent>().LoadAsync("texture.unity3d");
            //var list = (SpriteAtlas)ResourcesComponent.Instance.GetAsset("texture.unity3d", "UIDraw");
            var dic = FuluConfigCategory.Instance.GetAll();
            self.skillTglGroup.allowSwitchOff = true;
            for (int i = 0; i < self.toggles.Count; i++)
            {
                self.toggles[i].group = self.skillTglGroup;
                self.toggles[i].SetIsOnWithoutNotify(false);
                var load = ResourcesComponent.Instance.GetAsset("texture.unity3d", dic[i].Name);
                if (load.GetType() == typeof(Texture2D))
                {
                    var textload = load as Texture2D; ;
                    self.toggles[i].GetComponent<Image>().sprite = Sprite.Create(textload, new Rect(0, 0, textload.width, textload.height), Vector2.zero);
                }
                else
                {
                    self.toggles[i].GetComponent<Image>().sprite = load as Sprite;
                }
                self.toggles[i].onValueChanged.AddListener(self.onTglValueChanged);
            }
            self.selectBtn.onClick.AddListener(self.OnSelectBtn);
            self.backBtn.onClick.AddListener(self.OnBackBtn);
        }

        public static async ETTask InitSkill(this UISkillpanelComponent self)
        {
            await ETTask.CompletedTask;
            var list = FuluConfigCategory.Instance.GetAll();
            foreach (var item in list)
            {
                if (item.Value.Front[0] == -1)
                {
                    self.already.Add(item.Value.Id);
                }
            }
        }
        public static bool CheckQuali(this UISkillpanelComponent self, int id)
        {
            var list = FuluConfigCategory.Instance.Get(id);
            foreach (var item in list.Front)
            {
                if (!self.already.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }

        #region
        public static async void onTglValueChanged(this UISkillpanelComponent self, bool value)
        {
            for (int i = 0; i < self.toggles.Count; i++)
            {
                if (self.toggles[i].isOn)
                {
                    self.chosenId = i;
                    if (self.already.Contains(i))
                    {
                        return;
                    }
                    self.toggles[i].SetIsOnWithoutNotify(false);
                    if (!self.CheckQuali(i))
                    {
                        await UIHelper.Create(self.DomainScene(), UIType.UITips, UILayer.High);
                        var uitips = self.DomainScene().GetComponent<UIComponent>().Get(UIType.UITips);
                        uitips.GetComponent<UITipsComponent>().SetContent(2, "Please unlock the pre-skill first");
                    }
                    else
                    {
                        await UIHelper.Create(self.DomainScene(), UIType.UIConfirm, UILayer.High);
                        var uiConfirm = self.DomainScene().GetComponent<UIComponent>().Get(UIType.UIConfirm);
                        uiConfirm.GetComponent<UIConfirmComponent>().SetContent(i, "Are you sure you want to unlock?", self.OnYesBtn, self.OnNoBtn);
                    }
                }
            }
        }

        public static async void OnYesBtn(this UISkillpanelComponent self)
        {
            var uibagcomponent = self.DomainScene().GetComponent<UIComponent>().Get(UIType.UIBag).GetComponent<UIBagComponent>();
            if (!uibagcomponent.CheckUnlock(self.chosenId))
            {
                await UIHelper.Create(self.DomainScene(), UIType.UITips, UILayer.High);
                var uitips = self.DomainScene().GetComponent<UIComponent>().Get(UIType.UITips);
                uitips.GetComponent<UITipsComponent>().SetContent(2, "You don't have enough money");
                return;
            }
            uibagcomponent.CostUnlock(self.chosenId);
            self.already.Add(self.chosenId);
            self.toggles[self.chosenId].GetComponent<Image>().color = Color.white;
        }

        public static async void OnNoBtn(this UISkillpanelComponent self)
        {
            await ETTask.CompletedTask;
        }

        public static async void OnSelectBtn(this UISkillpanelComponent self)
        {
            if (!self.already.Contains(self.chosenId))
            {
                await UIHelper.Create(self.DomainScene(), UIType.UITips, UILayer.High);
                var uitips = self.DomainScene().GetComponent<UIComponent>().Get(UIType.UITips);
                uitips.GetComponent<UITipsComponent>().SetContent(3, "Please Select A Skill");
                return;
            }
            await UIHelper.Show(self.DomainScene(), UIType.UIDraw, UILayer.Mid);
            var uidraw = self.DomainScene().GetComponent<UIComponent>().Get(UIType.UIDraw);
            uidraw.GetComponent<UIDrawComponent>().GetFulu(self.chosenId).Coroutine();
            await UIHelper.Close(self.DomainScene(), UIType.UISkillpanel);
        }
        public static void OnBackBtn(this UISkillpanelComponent self)
        {
            UIHelper.Show(self.DomainScene(), UIType.UIGame, UILayer.Mid).Coroutine();
            UIHelper.Close(self.DomainScene(), UIType.UISkillpanel).Coroutine();
        }
        #endregion
    }
}
