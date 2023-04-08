using System;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

namespace ET
{
    [FriendClass(typeof(ET.HPComponent))]
    public static class HPComponentSystem
    {

        [ObjectSystem]
        public class AwakeSystem : AwakeSystem<HPComponent>
        {
            public override void Awake(HPComponent self)
            {
                self.uihp = self.ZoneScene().GetComponent<UIComponent>().Get(UIType.UIHP);
                self.gameObject = self.Parent.GetComponent<GameObjectComponent>().GameObject;
                self.uihp.GetComponent<UIHPComponent>().InitHP(self.Parent as Unit, 200, 7);

                self.maxhp = 100;
                self.hp = self.maxhp;
            }
        }

        [ObjectSystem]
        public class UpdateSystem : UpdateSystem<HPComponent>
        {
            public override void Update(HPComponent self)
            {
                self.uihp.GetComponent<UIHPComponent>().SetHP(self.gameObject, self.Parent as Unit, self.hp, self.maxhp, 200);
            }
        }

        [ObjectSystem]
        public class DestroySystem : DestroySystem<HPComponent>
        {
            public override void Destroy(HPComponent self)
            {
                self.uihp.GetComponent<UIHPComponent>().Recycle(self.gameObject, self.Parent as Unit);
            }
        }

        public static void SetHP(this HPComponent self, int hp)
        {
            self.hp = hp;
            if (self.hp <= 0)
            {
                Debug.LogWarning("已经死了");
            }
        }

        public static void GetDamage(this HPComponent self, int damage)
        {
            self.SetHP(self.hp - damage);
        }

    }
}