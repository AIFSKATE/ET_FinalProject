
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [ComponentOf(typeof(UI))]
    public class UILobbyComponent : Entity, IAwake
    {
        public GameObject enterMap;
        public TMP_InputField inputfield;
        public Text text;
    }
}
