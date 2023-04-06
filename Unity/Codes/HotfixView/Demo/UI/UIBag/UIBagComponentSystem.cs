using System;
using System.Collections.Generic;

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
            self.cell = rc.Get<GameObject>("Cell");
            self.celldic = new Dictionary<ICell, UIBagCellComponent>();

            self.scrollercomponent = rc.Get<GameObject>("GridUnlimitedScroller").GetComponent<GridUnlimitedScroller>();
            self.bgimage = rc.Get<GameObject>("BGImage").GetComponent<Image>();
            self.backbtn = rc.Get<GameObject>("BackBtn").GetComponent<Button>();
            self.bgimage.type = Image.Type.Sliced;



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

            self.scrollercomponent.ETInstantiate += self.ETInstantiate;
            //self.scrollercomponent.ETDestroyAllCells += self.ETDestroyAllCells;


            self.scrollercomponent.Generate(self.cell, 100, (index, iCell) =>
            {
                self.celldic[iCell].OnGenerated(index, iCell);
            });

            await ETTask.CompletedTask;
        }

        public static void OnBackBtn(this UIBagComponent self)
        {
            UIHelper.Show(self.DomainScene(), UIType.UIGame, UILayer.Mid).Coroutine();
            UIHelper.Close(self.DomainScene(), UIType.UIBag).Coroutine();
        }

        public static GameObject ETInstantiate(this UIBagComponent self, GameObject cell, RectTransform rectTransform)
        {
            var res = GameObject.Instantiate(cell, rectTransform);
            var child = self.AddChild<UIBagCellComponent, GameObject>(res);
            self.celldic.Add(res.GetComponent<ICell>(), child);
            return res;
        }

        public static void ETDestroyAllCells(this UIBagComponent self)
        {
            self.celldic.Clear();
            self.Children.Clear();
        }
    }
}
