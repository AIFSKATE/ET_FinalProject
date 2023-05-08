using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace ET
{
    [ComponentOf(typeof(UI))]
    public class UISkillpanelComponent : Entity, IAwake, IUpdate
    {
        public Toggle skillTgl_1;
        public Toggle skillTgl_2;
        public Toggle skillTgl_3;
        public Toggle skillTgl_4;
        public Toggle skillTgl_5;
        public Toggle skillTgl_6;
        public Toggle skillTgl_7;
        public List<Toggle> toggles;
        public int chosenId;
        public HashSet<int> already;

        public ToggleGroup skillTglGroup;
        public Button selectBtn;
        public Button backBtn;

        public Image titleImage;
        public SpriteAtlas imagelist;
        public SpriteAtlas uilist;
    }
}
