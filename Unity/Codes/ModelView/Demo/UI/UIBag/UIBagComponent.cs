using System;
using System.Collections.Generic;
using System.Net;

using UnityEngine;
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

        public Dictionary<ICell, UIBagCellComponent> celldic;
    }
}
