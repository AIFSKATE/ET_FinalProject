using UnityEngine;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class GameObjectComponent : Entity, IAwake<GameObject>, IDestroy
    {
        public GameObject GameObject { get; set; }
        public DelegateMonoBehaviour delegateCollider { get; set; }
    }
}