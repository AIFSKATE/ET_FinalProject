using UnityEngine;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class SkeletonMonoComponent : Entity, IAwake, IDestroy
    {
        public DelegateMonoBehaviour delegateCollider { get; set; }
    }

    public static class SkeletonMonoComponentSystem
    {

        [ObjectSystem]
        public class AwakeSystem : AwakeSystem<SkeletonMonoComponent>
        {
            public override void Awake(SkeletonMonoComponent self)
            {
                GameObjectComponent gameObjectComponent = self.Parent.GetComponent<GameObjectComponent>();
                self.delegateCollider = gameObjectComponent.delegateCollider;
                self.delegateCollider.on_TriggerEnter += OnTriggerEnter;
            }

            private void OnTriggerEnter(Collider other)
            {
                Debug.LogWarning("碰撞了");
            }
        }
        [ObjectSystem]
        public class DestroySystem : DestroySystem<SkeletonMonoComponent>
        {
            public override void Destroy(SkeletonMonoComponent self)
            {

            }
        }
    }
}