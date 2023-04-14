using System;
using System.Collections.Generic;
using System.Net;

using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnlimitedScrollUI;

namespace ET
{
    [ComponentOf(typeof(UI))]
    [ChildType(typeof(UIBagCellComponent))]
    public class UIBagComponent : Entity, IAwake
    {
        public GridUnlimitedScroller scrollercomponent;
        public Image bgimage;
        public Button backbtn;
        public GameObject cell;
        public SpriteAtlas imagelist;
        public UI uigame;

        public Dictionary<ICell, UIBagCellComponent> celldic;
        public List<Iteminfo> havelist;
        public Dictionary<int, int> havedic;
    }

    public class Iteminfo
    {
        public int itemid;
        public int num;
        public Iteminfo(int itemid, int num)
        {
            this.itemid = itemid;
            this.num = num;
        }
    }
}
