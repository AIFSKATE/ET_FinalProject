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
        public override void AwakeAsync(UIBagComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            self.cell = rc.Get<GameObject>("Cell");
            self.celldic = new Dictionary<ICell, UIBagCellComponent>();
            self.havelist = new List<Iteminfo>();
            self.havedic = new Dictionary<int, int>();
            self.imagelist = (SpriteAtlas)ResourcesComponent.Instance.GetAsset("uisprite.unity3d", "Pixel");


            self.uigame = self.DomainScene().GetComponent<UIComponent>().Get(UIType.UIGame);
            self.scrollercomponent = rc.Get<GameObject>("GridUnlimitedScroller").GetComponent<GridUnlimitedScroller>();
            self.bgimage = rc.Get<GameObject>("BGImage").GetComponent<Image>();
            self.backbtn = rc.Get<GameObject>("BackBtn").GetComponent<Button>();
            self.bgimage.type = Image.Type.Sliced;

            for (int i = 0; i < 11; i++)
            {
                self.AddItem(i, 10);
            }


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
                self.celldic[iCell].OnGenerated(index, iCell, self.havelist, self.imagelist);
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

        public static void AddItem(this UIBagComponent self, int itemid, int num)
        {
            var item = new Iteminfo(itemid, num);
            if (!self.havedic.TryGetValue(itemid, out int nowhave))
            {
                self.havelist.Add(item);
                self.havedic[itemid] = self.havelist.Count - 1;
            }
            else
            {
                self.havelist[self.havedic[itemid]].num += num;
            }

            if (self.havelist[self.havedic[itemid]].num <= 0)
            {
                var index = self.havedic[itemid];
                self.havelist.RemoveAt(index);
                self.havedic.Remove(itemid);
                foreach (var t in self.havelist)
                {
                    if (self.havedic[t.itemid] >= index)
                    {
                        self.havedic[t.itemid]--;
                    }
                }
            }
        }

        public static void Refresh(this UIBagComponent self)
        {
            self.scrollercomponent.DestroyAllCells();
            self.scrollercomponent.GenerateAllCells();
        }

        public static void SubtractItem(this UIBagComponent self, int itemid, int num)
        {

        }

        public static void CostAndGenerate(this UIBagComponent self, int fuluid)
        {
            var costlist = FuluConfigCategory.Instance.Get(fuluid).Need;
            for (int i = 0; i < costlist.Length; i += 2)
            {
                self.AddItem(costlist[i], -costlist[i + 1]);
            }
            var generateid = FuluConfigCategory.Instance.Get(fuluid).Generate;
            if (generateid >= 0)
            {
                self.uigame.GetComponent<UIGameComponent>().Generate(generateid);
            }
            return;
        }

        public static bool CheckEnough(this UIBagComponent self, int fuluid)
        {
            var costlist = FuluConfigCategory.Instance.Get(fuluid).Need;
            for (int i = 0; i < costlist.Length; i += 2)
            {
                if (self.GetNum(costlist[i]) < costlist[i + 1])
                {
                    return false;
                }
            }
            return true;
        }
        public static bool CheckUnlock(this UIBagComponent self, int fuluid)
        {
            var costlist = FuluConfigCategory.Instance.Get(fuluid).Unlockarr;
            for (int i = 0; i < costlist.Length; i += 2)
            {
                if (self.GetNum(costlist[i]) < costlist[i + 1])
                {
                    return false;
                }
            }
            return true;
        }
        public static void CostUnlock(this UIBagComponent self, int fuluid)
        {
            var costlist = FuluConfigCategory.Instance.Get(fuluid).Unlockarr;
            for (int i = 0; i < costlist.Length; i += 2)
            {
                self.AddItem(costlist[i], -costlist[i + 1]);
            }
        }

        public static int GetNum(this UIBagComponent self, int itemid)
        {
            if (self.havedic.ContainsKey(itemid))
            {
                return self.havelist[self.havedic[itemid]].num;
            }
            return 0;
        }
    }
}
