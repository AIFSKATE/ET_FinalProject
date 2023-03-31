using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace ET
{
    [ObjectSystem]
    public class UISkillpanelComponentAwakeSystem : AwakeSystem<UISkillpanelComponent>
    {
        public override void Awake(UISkillpanelComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            self.chosenId = -1;
            self.selectBtn = rc.Get<GameObject>("SelectBtn").GetComponent<Button>();
            self.skillTglGroup = rc.Get<GameObject>("TglGroup").GetComponent<ToggleGroup>();
            self.skillTgl_1 = rc.Get<GameObject>("Skill1").GetComponent<Toggle>();
            self.skillTgl_2 = rc.Get<GameObject>("Skill2").GetComponent<Toggle>();
            self.skillTgl_3 = rc.Get<GameObject>("Skill3").GetComponent<Toggle>();
            self.skillTgl_4 = rc.Get<GameObject>("Skill4").GetComponent<Toggle>();
            self.skillTgl_5 = rc.Get<GameObject>("Skill5").GetComponent<Toggle>();

            self.toggles = new List<Toggle>();
            self.toggles.Add(self.skillTgl_1);
            self.toggles.Add(self.skillTgl_2);
            self.toggles.Add(self.skillTgl_3);
            self.toggles.Add(self.skillTgl_4);
            self.toggles.Add(self.skillTgl_5);

            self.BindListener().Coroutine();
        }
    }



    [FriendClass(typeof(UISkillpanelComponent))]
    public static class UISkillpanelComponentSystem
    {
        public static async ETTask BindListener(this UISkillpanelComponent self)
        {
            await self.DomainScene().GetComponent<ResourcesLoaderComponent>().LoadAsync("texture.unity3d");
            var list = (SpriteAtlas)ResourcesComponent.Instance.GetAsset("texture.unity3d", "UIDraw");
            var dic = FuluConfigCategory.Instance.GetAll();
            self.skillTglGroup.allowSwitchOff = false;
            for (int i = 0; i < self.toggles.Count; i++)
            {
                self.toggles[i].group = self.skillTglGroup;
                self.toggles[i].GetComponent<Image>().sprite = list.GetSprite(dic[i + 1].Name);
                self.toggles[i].onValueChanged.AddListener(self.onTglValueChanged);
            }
            self.selectBtn.onClick.AddListener(self.OnSelectBtn);
        }

        #region
        public static void onTglValueChanged(this UISkillpanelComponent self, bool value)
        {
            for (int i = 0; i < self.toggles.Count; i++)
            {
                if (self.toggles[i].isOn)
                {
                    self.chosenId = i + 1;
                    return;
                }
            }
        }

        public static void OnSelectBtn(this UISkillpanelComponent self)
        {
            if (self.chosenId == -1)
            {
                UIHelper.Create(self.DomainScene(), UIType.UITips, UILayer.High).Coroutine();
            }

        }
        #endregion
    }
}
