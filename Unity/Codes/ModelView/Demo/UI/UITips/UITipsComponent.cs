using System;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [ComponentOf(typeof(UI))]
    public class UITipsComponent : Entity, IAwake
    {
        public TextMeshProUGUI Text;
    }
}
