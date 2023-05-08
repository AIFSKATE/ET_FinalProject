using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace ET
{
    [ComponentOf(typeof(UI))]
    public class UIDrawComponent : Entity, IAwake, IUpdate
    {
        public Image fulu;
        public RawImage mydraw;
        public Material DrawWord;
        public Material Clear;
        public DrawComponent drawComponent;
        public RectTransform canvasRect;
        public RectTransform contentRect;
        public RectTransform generateContentRect;
        public GameObject needItem;
        public Color brushColor = Color.black;
        public Camera MyCamera;
        public Button checkBtn;
        public Button backBtn;
        public Button clearBtn;
        public Button LittlesizeButton;
        public Button MiddlesizeButton;
        public Button BigsizeButton;
        public TextMeshProUGUI similarity;
        public TextMeshProUGUI checktext;
        public PHashComponent phashcomponent;
        public Texture2D fuluTex;
        public SpriteAtlas imagelist;
        public UI uibag;
        public UI uigame;

        public int pensize = 100;
        public int texsize;
        public int chosenid;
        public int res;
        public readonly Color[] myColor = new Color[] { Color.black, Color.red, Color.green, Color.yellow };
    }
}
