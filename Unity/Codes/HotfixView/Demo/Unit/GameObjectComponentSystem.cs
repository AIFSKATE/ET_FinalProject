using System;
using UnityEngine;

namespace ET
{
    public static class GameObjectComponentSystem
    {

        [ObjectSystem]
        public class AwakeSystem : AwakeSystem<GameObjectComponent, GameObject>
        {
            public override void Awake(GameObjectComponent self, GameObject gameObject)
            {
                if (gameObject == null) return;
                self.GameObject = gameObject;
                DelegateMonoBehaviour _delegateCollider = gameObject.GetComponent<DelegateMonoBehaviour>();
                if (_delegateCollider == null)
                {
                    _delegateCollider = gameObject.AddComponent<DelegateMonoBehaviour>();
                }
                _delegateCollider.BelongToUnitId = self.Id;
                self.delegateCollider = _delegateCollider;
            }
        }
        [ObjectSystem]
        public class DestroySystem : DestroySystem<GameObjectComponent>
        {
            public override void Destroy(GameObjectComponent self)
            {
                self.delegateCollider = null;
                UnityEngine.Object.Destroy(self.GameObject);
            }
        }
    }
}