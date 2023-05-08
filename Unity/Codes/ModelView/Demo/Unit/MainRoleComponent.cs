using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public enum NumType
    {
        damage,
        hp,
        defense,
        maxhp,
    }
    [ComponentOf(typeof(Unit))]
    public class MainRoleComponent : Entity, IAwake, IDestroy
    {
        public static MainRoleComponent Instance { get; set; }
        public Dictionary<int, int> dic;
        public bool dead;
        public UIGameComponent uigamecomponent;
    }
}