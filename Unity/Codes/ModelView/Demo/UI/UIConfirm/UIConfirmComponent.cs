using System;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace ET
{
    [ComponentOf(typeof(UI))]
    public class UIConfirmComponent : Entity, IAwake
    {
        public TextMeshProUGUI Text;
        public Toggle yestgl;
        public Toggle notgl;
        public RectTransform contentRect;
        public SpriteAtlas imagelist;
        public UI uibag;

        public Action Yes;
        public Action No;
    }
}
