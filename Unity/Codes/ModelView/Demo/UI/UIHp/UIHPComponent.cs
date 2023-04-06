using System;
using System.Collections.Generic;
using System.Net;

using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [ComponentOf(typeof(UI))]
    public class UIHPComponent : Entity, IAwake
    {
        public RectTransform panel;
        public GameObject hpimage;
        public Dictionary<Unit, RectTransform> dichp;
    }
}
