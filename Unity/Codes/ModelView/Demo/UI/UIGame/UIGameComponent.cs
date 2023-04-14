using System;
using System.Collections.Generic;
using System.Net;

using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace ET
{
    [ComponentOf(typeof(UI))]
    public class UIGameComponent : Entity, IAwake
    {
        public Button ShowUIDrawBtn;
        public Button ShowUIBagBtn;
        public RectTransform consumablePanel;
        public RectTransform skillPanel;
        public List<ReferenceCollector> skills;
        public List<int> numlist;
        public SpriteAtlas imagelist;
    }
}
