using System;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [ComponentOf(typeof(UI))]
    public class UIMainComponent : Entity, IAwake
    {
        public Button startBtn;
    }
}
