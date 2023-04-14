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
        max,
    }
    [ComponentOf(typeof(Unit))]
    public class MainRoleComponent : Entity, IAwake, IDestroy, IUpdate
    {
        public static MainRoleComponent Instance;
        public Dictionary<int, int> dic;
    }
}