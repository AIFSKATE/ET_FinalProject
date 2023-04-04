using System;
using System.Net;

using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnlimitedScrollUI;

namespace ET
{
    [ObjectSystem]
    public class UIBagComponentAwakeSystem : AwakeSystem<UIBagComponent>
    {
        public override void Awake(UIBagComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            GameObject Cell = rc.Get<GameObject>("Cell");
            self.scrollercomponent = rc.Get<GameObject>("GridUnlimitedScroller").GetComponent<GridUnlimitedScroller>();
            self.bgimage = rc.Get<GameObject>("BGImage").GetComponent<Image>();
            self.backbtn = rc.Get<GameObject>("BackBtn").GetComponent<Button>();
            self.bgimage.type = Image.Type.Sliced;

            self.scrollercomponent.Generate(Cell, 100, (index, iCell) =>
            {
                var regularCell = iCell as RegularCell;
                if (regularCell != null) regularCell.onGenerated?.Invoke(index);
            });

            self.BindListener().Coroutine();
        }
    }

    [FriendClass(typeof(UIBagComponent))]
    public static class UIBagComponentSystem
    {
        public static async ETTask BindListener(this UIBagComponent self)
        {
            var list = (SpriteAtlas)ResourcesComponent.Instance.GetAsset("uisprite.unity3d", "uisprite");
            self.bgimage.sprite = list.GetSprite("GUI_27");

            self.backbtn.GetComponent<Image>().sprite = list.GetSprite("GUI_67");
            self.backbtn.onClick.AddListener(self.OnBackBtn);

            await ETTask.CompletedTask;
        }

        public static void OnBackBtn(this UIBagComponent self)
        {
            UIHelper.Show(self.DomainScene(), UIType.UIGame, UILayer.Mid).Coroutine();
            UIHelper.Close(self.DomainScene(), UIType.UIBag).Coroutine();
        }
    }
}
