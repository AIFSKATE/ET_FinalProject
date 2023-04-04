using System;
using System.Net;

using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [ComponentOf(typeof(UI))]
    public class UIGameComponent : Entity, IAwake
    {
        public Button ShowUIDrawBtn;
        public Button ShowUIBagBtn;
    }
}
