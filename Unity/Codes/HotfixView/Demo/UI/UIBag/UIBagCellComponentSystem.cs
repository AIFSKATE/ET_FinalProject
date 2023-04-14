using System;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnlimitedScrollUI;

namespace ET
{
    [ObjectSystem]
    public class UIBagCellComponentAwakeSystem : AwakeSystem<UIBagCellComponent, GameObject>
    {
        public override void Awake(UIBagCellComponent self, GameObject gameObject)
        {
            self.gameObject = gameObject;
            ReferenceCollector rc = self.gameObject.GetComponent<ReferenceCollector>();
            self.image = rc.Get<GameObject>("Image").GetComponent<Image>();
            self.itemimg = rc.Get<GameObject>("Item").GetComponent<Image>();
            self.text = rc.Get<GameObject>("Text").GetComponent<TextMeshProUGUI>();


            self.cell = self.gameObject.GetComponent<UIBagCell>();
            if (self.cell == null)
            {
                self.cell = self.gameObject.AddComponent<UIBagCell>();
            }

            self.cell.on_BecomeInvisible += self.OnBecomeInvisible;
            self.cell.on_BecomeVisible += self.OnBecomeVisible;
            self.BindListener().Coroutine();
        }
    }

    [ObjectSystem]
    public class UIBagCellComponentDestorySystem : DestroySystem<UIBagCellComponent>
    {
        public override void Destroy(UIBagCellComponent self)
        {
            GameObject.Destroy(self.gameObject);
        }
    }

    [FriendClass(typeof(UIBagCellComponent))]
    public static class UIBagCellComponentSystem
    {
        public static async ETTask BindListener(this UIBagCellComponent self)
        {
            var list = (SpriteAtlas)ResourcesComponent.Instance.GetAsset("uisprite.unity3d", "uisprite");
            self.image.sprite = list.GetSprite("GUI_53");

            await ETTask.CompletedTask;
        }

        public static void OnBecomeInvisible(this UIBagCellComponent self, ScrollerPanelSide side)
        {

        }
        public static void OnBecomeVisible(this UIBagCellComponent self, ScrollerPanelSide side)
        {

        }
        public static void OnGenerated(this UIBagCellComponent self, int index, ICell iCell, List<Iteminfo> havelist, SpriteAtlas imagelist)
        {
            self.itemimg.enabled = false;
            self.text.enabled = false;
            if (index >= havelist.Count)
            {
                return;
            }
            self.itemimg.enabled = true;
            self.text.enabled = true;
            self.text.text = $"{havelist[index].num}";
            self.itemimg.sprite = imagelist.GetSprite(ItemConfigCategory.Instance.Get(havelist[index].itemid).Name);
            //uIBagCell.GetComponent<ReferenceCollector>().Get<GameObject>("Text").GetComponent<TextMeshProUGUI>().text = $"{index}";
        }
    }
}
