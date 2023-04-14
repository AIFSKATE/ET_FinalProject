using System;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

namespace ET
{
    [FriendClass(typeof(ET.MainRoleComponent))]
    public static class MainRoleComponentSystem
    {
        [ObjectSystem]
        public class MainRoleAwakeSystem : AwakeSystem<MainRoleComponent>
        {
            public override void AwakeAsync(MainRoleComponent self)
            {
                MainRoleComponent.Instance = self;
                self.dic = new System.Collections.Generic.Dictionary<int, int>();
                self.dic[(int)NumType.damage] = 20;
            }
        }

        [ObjectSystem]
        public class MainRoleDestroySystem : DestroySystem<MainRoleComponent>
        {
            public override void Destroy(MainRoleComponent self)
            {

            }
        }

        [ObjectSystem]
        public class MainRoleUpdateSystem : UpdateSystem<MainRoleComponent>
        {
            public override void Update(MainRoleComponent self)
            {

            }
        }

        public static void ChangeNum(this MainRoleComponent self, int type, int num)
        {
            Debug.LogWarning("changeNum" + $"type {type}" + $"num {num}");
            self.dic.TryGetValue(type, out int t);
            self.dic[type] = t + num;
        }

        public static int GetNum(this MainRoleComponent self, int type)
        {
            self.dic.TryGetValue(type, out int t);

            return t;
        }
    }

}