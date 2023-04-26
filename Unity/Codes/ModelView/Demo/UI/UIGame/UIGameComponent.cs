using System;
using System.Collections.Generic;
using System.Net;
using TMPro;
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
        public List<float> cooltime;
        public List<bool> canconsume;
        public SpriteAtlas imagelist;
        public Image playerHP;
        public Image playerHPBar;
        public Image playerHPBarBg;
        public TextMeshProUGUI htTmp;
        public TextMeshProUGUI attackTmp;
        public TextMeshProUGUI defenseTmp;

        public TextMeshProUGUI countdown;
        public GameObject PanelParent;
        public GameObject TipsParent;
        public GameObject Panel;
        public GameObject Cell;
        public Image Title;
        public SpriteAtlas uilist;
        public Button YesButton;
        public UI uibag;
    }
}
