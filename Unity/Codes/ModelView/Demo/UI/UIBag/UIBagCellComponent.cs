using System;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnlimitedScrollUI;

namespace ET
{
    [ComponentOf(typeof(UIBagComponent))]
    public class UIBagCellComponent : Entity, IAwake<GameObject>
    {
        public GameObject gameObject;
        public UIBagCell cell;
        public Image image;
        public TextMeshProUGUI text;
    }
}
