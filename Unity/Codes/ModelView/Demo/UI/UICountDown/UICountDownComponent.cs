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
    [ComponentOf(typeof(UI))]
    public class UICountDownComponent : Entity, IAwake
    {
        public TextMeshProUGUI countdown;
        public GameObject PanelParent;
        public GameObject Panel;
        public GameObject Cell;
        public SpriteAtlas imagelist;
        public SpriteAtlas uilist;
        public Button YesButton;
        public UI uibag;
    }
}
