using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{

    [ObjectSystem]
    public class AwakeSystem : AwakeSystem<TriggerComponent>
    {
        public override void AwakeAsync(TriggerComponent self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class DestroySystem : DestroySystem<TriggerComponent>
    {
        public override void Destroy(TriggerComponent self)
        {
            self.boxCollider = null;
        }
    }


    [FriendClass(typeof(TriggerComponent))]
    public static class TriggerComponentSystem
    {
        public static void Awake(this TriggerComponent self)
        {
            BoxCollider boxCollider_1 = self.Parent.GetComponent<GameObjectComponent>().GameObject.GetComponent<BoxCollider>();
            if (boxCollider_1 == null)
            {
                return;
            }
            if (boxCollider_1.isTrigger == false)
            {
                return;
            }
            self.boxCollider = boxCollider_1;
        }

    }
}
