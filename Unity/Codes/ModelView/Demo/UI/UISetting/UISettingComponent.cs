using System;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [ComponentOf(typeof(UI))]
    public class UISettingComponent : Entity, IAwake
    {
        public Slider volumeslider;
        public Button exitpanel;
    }
}
