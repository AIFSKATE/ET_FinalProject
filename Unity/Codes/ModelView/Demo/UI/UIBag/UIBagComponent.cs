using System;
using System.Net;

using UnityEngine;
using UnityEngine.UI;
using UnlimitedScrollUI;

namespace ET
{
    [ComponentOf(typeof(UI))]
    public class UIBagComponent : Entity, IAwake
    {
        public GridUnlimitedScroller scrollercomponent;
        public Image bgimage;
        public Button backbtn;
        public UIBagCellComponent cellcomponent;
    }
}
