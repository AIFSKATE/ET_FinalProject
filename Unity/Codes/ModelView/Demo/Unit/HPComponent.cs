using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class HPComponent : Entity, IAwake, IDestroy, IUpdate
    {
        public Image hpimage;
        public GameObject gameObject;
        public UI uihp;
    }
}