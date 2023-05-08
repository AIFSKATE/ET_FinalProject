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
                MainRoleComponent.Instance.Awake();
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
            MainRoleComponent.Instance.dic?.Clear();
            MainRoleComponent.Instance.dic = new System.Collections.Generic.Dictionary<int, int>();
            MainRoleComponent.Instance.uigamecomponent = MainRoleComponent.Instance.ZoneScene().GetComponent<UIComponent>().Get(UIType.UIGame).GetComponent<UIGameComponent>();
            MainRoleComponent.Instance.dead = false;

            MainRoleComponent.Instance.ChangeNum((int)NumType.maxhp, 100);
            MainRoleComponent.Instance.ChangeNum((int)NumType.damage, 20);
            MainRoleComponent.Instance.ChangeNum((int)NumType.hp, 100);
            MainRoleComponent.Instance.ChangeNum((int)NumType.defense, 2);
        }

        public static void ChangeNum(this MainRoleComponent self, int type, int num)
        {
            switch ((NumType)type)
            {
                case NumType.damage: MainRoleComponent.Instance.ChangeDamage(type, num); break;
                case NumType.hp: MainRoleComponent.Instance.ChangeHP(type, num); break;
                case NumType.maxhp: MainRoleComponent.Instance.ChangeMaxHP(type, num); break;
                case NumType.defense: MainRoleComponent.Instance.ChangeDefense(type, num); break;
            }
        }
        private static void ChangeDamage(this MainRoleComponent self, int type, int num)
        {
            MainRoleComponent.Instance.dic.TryGetValue(type, out int t);
            MainRoleComponent.Instance.dic[type] = t + num;
            if (MainRoleComponent.Instance.dic[type] < 0)
            {
                MainRoleComponent.Instance.dic[type] = 0;
            }
            MainRoleComponent.Instance.uigamecomponent.RefreshAttack(MainRoleComponent.Instance.dic[type]);
        }
        private static void ChangeHP(this MainRoleComponent self, int type, int num)
        {
            if (!MainRoleComponent.Instance.dic.ContainsKey((int)NumType.maxhp))
            {
                MainRoleComponent.Instance.dic[(int)NumType.maxhp] = 0;
            }
            MainRoleComponent.Instance.dic.TryGetValue(type, out int t);
            MainRoleComponent.Instance.dic[type] = t + num;

            MainRoleComponent.Instance.dic.TryGetValue((int)NumType.maxhp, out int maxhp);
            if (MainRoleComponent.Instance.dic[type] > maxhp)
            {
                MainRoleComponent.Instance.dic[type] = maxhp;
            }
            if (!MainRoleComponent.Instance.dead && MainRoleComponent.Instance.dic[type] <= 0)
            {
                MainRoleComponent.Instance.dead = true;
                MainRoleComponent.Instance.ZoneScene().CurrentScene().GetComponent<LevelComponent>().EndLevel().Coroutine();
            }
            MainRoleComponent.Instance.uigamecomponent.RefreshHP(MainRoleComponent.Instance.dic[type], MainRoleComponent.Instance.dic[(int)NumType.maxhp]);
        }
        private static void ChangeMaxHP(this MainRoleComponent self, int type, int num)
        {
            if (!MainRoleComponent.Instance.dic.ContainsKey((int)NumType.hp))
            {
                MainRoleComponent.Instance.dic[(int)NumType.hp] = 0;
            }
            MainRoleComponent.Instance.dic.TryGetValue(type, out int t);
            MainRoleComponent.Instance.dic[type] = t + num;

            MainRoleComponent.Instance.dic.TryGetValue((int)NumType.hp, out int hp);
            if (num < 0)
            {
                if (hp > MainRoleComponent.Instance.dic[type])
                {
                    MainRoleComponent.Instance.ChangeNum((int)NumType.hp, MainRoleComponent.Instance.dic[type] - hp);
                }
            }
            MainRoleComponent.Instance.uigamecomponent.RefreshHP(MainRoleComponent.Instance.dic[(int)NumType.hp], MainRoleComponent.Instance.dic[type]);
        }
        private static void ChangeDefense(this MainRoleComponent self, int type, int num)
        {
            MainRoleComponent.Instance.dic.TryGetValue(type, out int t);
            MainRoleComponent.Instance.dic[type] = t + num;
            MainRoleComponent.Instance.uigamecomponent.RefreshDefense(MainRoleComponent.Instance.dic[type]);
        }

        public static int GetNum(this MainRoleComponent self, int type)
        {
            MainRoleComponent.Instance.dic.TryGetValue(type, out int t);

            return t;
        }
    }

}