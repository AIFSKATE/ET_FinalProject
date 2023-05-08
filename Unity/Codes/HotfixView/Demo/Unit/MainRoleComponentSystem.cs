using System;
using System.Collections.Generic;
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
                self.Awake();
            }
        }

        [ObjectSystem]
        public class MainRoleDestroySystem : DestroySystem<MainRoleComponent>
        {
            public override void Destroy(MainRoleComponent self)
            {
                self.dic.Clear();
            }
        }

        //[ObjectSystem]
        //public class MainRoleUpdateSystem : UpdateSystem<MainRoleComponent>
        //{
        //    public override void Update(MainRoleComponent self)
        //    {

        //    }
        //}

        public static void Awake(this MainRoleComponent self)
        {
            self.dic?.Clear();
            self.dic = new System.Collections.Generic.Dictionary<int, int>();
            self.uigamecomponent = self.ZoneScene().GetComponent<UIComponent>().Get(UIType.UIGame).GetComponent<UIGameComponent>();
            self.dead = false;

            self.ChangeNum((int)NumType.maxhp, 100);
            self.ChangeNum((int)NumType.damage, 20);
            self.ChangeNum((int)NumType.hp, 100);
            self.ChangeNum((int)NumType.defense, 2);
        }

        public static void ChangeNum(this MainRoleComponent self, int type, int num)
        {
            switch ((NumType)type)
            {
                case NumType.damage: self.ChangeDamage(type, num); break;
                case NumType.hp: self.ChangeHP(type, num); break;
                case NumType.maxhp: self.ChangeMaxHP(type, num); break;
                case NumType.defense: self.ChangeDefense(type, num); break;
            }
        }
        private static void ChangeDamage(this MainRoleComponent self, int type, int num)
        {
            self.dic.TryGetValue(type, out int t);
            self.dic[type] = t + num;
            if (self.dic[type] < 0)
            {
                self.dic[type] = 0;
            }
            self.uigamecomponent.RefreshAttack(self.dic[type]);
        }
        private static void ChangeHP(this MainRoleComponent self, int type, int num)
        {
            if (!self.dic.ContainsKey((int)NumType.maxhp))
            {
                self.dic[(int)NumType.maxhp] = 0;
            }
            self.dic.TryGetValue(type, out int t);
            self.dic[type] = t + num;

            self.dic.TryGetValue((int)NumType.maxhp, out int maxhp);
            if (self.dic[type] > maxhp)
            {
                self.dic[type] = maxhp;
            }
            if (!self.dead && self.dic[type] <= 0)
            {
                self.dead = true;
                self.ZoneScene().CurrentScene().GetComponent<LevelComponent>().EndLevel().Coroutine();
            }
            self.uigamecomponent.RefreshHP(self.dic[type], self.dic[(int)NumType.maxhp]);
        }
        private static void ChangeMaxHP(this MainRoleComponent self, int type, int num)
        {
            if (!self.dic.ContainsKey((int)NumType.hp))
            {
                self.dic[(int)NumType.hp] = 0;
            }
            self.dic.TryGetValue(type, out int t);
            self.dic[type] = t + num;

            self.dic.TryGetValue((int)NumType.hp, out int hp);
            if (num < 0)
            {
                if (hp > self.dic[type])
                {
                    self.ChangeNum((int)NumType.hp, self.dic[type] - hp);
                }
            }
            self.uigamecomponent.RefreshHP(self.dic[(int)NumType.hp], self.dic[type]);
        }
        private static void ChangeDefense(this MainRoleComponent self, int type, int num)
        {
            self.dic.TryGetValue(type, out int t);
            self.dic[type] = t + num;
            self.uigamecomponent.RefreshDefense(self.dic[type]);
        }

        public static int GetNum(this MainRoleComponent self, int type)
        {
            self.dic.TryGetValue(type, out int t);

            return t;
        }
    }

}