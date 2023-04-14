using System;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnlimitedScrollUI;

namespace ET
{
    public class UIBagCellComponent : Entity, IAwake<GameObject>, IDestroy
    {
        public GameObject gameObject;
        public UIBagCell cell;
        public Image image;
        public Image itemimg;
        public TextMeshProUGUI text;
    }
}
