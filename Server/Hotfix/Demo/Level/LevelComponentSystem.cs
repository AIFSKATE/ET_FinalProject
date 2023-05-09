using SharpCompress.Common;
using System.Collections.Generic;
using System.ServiceModel.Channels;

namespace ET
{
    //Server
    [ObjectSystem]
    public class LevelComponentAwakeSystem : AwakeSystem<LevelComponent>
    {
        public override void AwakeAsync(LevelComponent self)
        {
            self.endlevel = 3;
            self.playernum = 0;
            self.enemylist = new System.Collections.Generic.List<long>();
        }
    }
    [FriendClass(typeof(ET.LevelComponent))]
    public static class LevelComponentSystem
    {
        public static int GetEndLevel(this LevelComponent self)
        {
            return self.endlevel;
        }
        public static int GetNowLevel(this LevelComponent self)
        {
            return self.nowlevel;
        }
        public static void SetNowLevel(this LevelComponent self, int nowlevel)
        {
            self.nowlevel = nowlevel;
        }
        public static void Clear(this LevelComponent self)
        {
            self.enemylist.Clear();
            self.playernum = 0;
        }

        public static void Prepare(this LevelComponent self)
        {
            self.SetNowLevel(0);
            self.playernum++;
            if (self.playernum == 1)
            {
                self.NextLevel();
            }
        }
        public static async void NextLevel(this LevelComponent self)
        {
            self.nowlevel += 1;
            if (self.nowlevel > self.endlevel)
            {
                self.playernum = 0;
                foreach (var item in self.DomainScene().GetComponent<UnitComponent>().GetPlauerList())
                {
                    MessageHelper.SendToClient(item, new M2C_EndLevel());
                }
            }
            else
            {
                if (self.nowlevel != 1)
                {
                    foreach (var item in self.DomainScene().GetComponent<UnitComponent>().GetPlauerList())
                    {
                        MessageHelper.SendToClient(item, new M2C_PrepareTheNext() { time = 40 });
                    }
                    await TimerComponent.Instance.WaitAsync(40000);
                }
                self.StartLevel(self.nowlevel);
            }
        }

        public static void RemoveEnemy(this LevelComponent self, long id)
        {
            var unitcomponent = self.DomainScene().GetComponent<UnitComponent>();
            unitcomponent.Remove(id);
            self.enemylist.Remove(id);

            if (self.enemylist.Count == 0)
            {
                self.NextLevel();
            }
        }

        public static void SubtractPlayer(this LevelComponent self)
        {
            self.playernum--;
            if (self.playernum == 0)
            {
                var unitcomponent = self.DomainScene().GetComponent<UnitComponent>();
                var levelcomponent = self;
                levelcomponent.Clear();

                int count = unitcomponent.Children.Count;
                List<long> tidlist = new List<long>();
                foreach (var item in unitcomponent.Children)
                {
                    if ((item.Value as Unit).Type != UnitType.Player)
                    {
                        tidlist.Add(item.Key);
                    }
                }
                foreach (var item in tidlist)
                {
                    unitcomponent.Remove(item);
                }
                tidlist.Clear();
                tidlist = null;
            }
        }

        public static void StartLevel(this LevelComponent self, int nowlevel)
        {
            UnitComponent unitComponent = self.DomainScene().GetComponent<UnitComponent>();
            UnitConfig unitConfig_1 = UnitConfigCategory.Instance.Get(1002);
            for (int i = 0; i < 4; i++)
            {
                Unit unitenemy = UnitFactory.Create(self.DomainScene(), unitConfig_1.Id, UnitType.Monster);
                unitComponent.Add(unitenemy);
                self.enemylist.Add(unitenemy.Id);
            }
            UnitConfig unitConfig_2 = UnitConfigCategory.Instance.Get(1003);
            for (int i = 0; i < 4; i++)
            {
                Unit unitenemy = UnitFactory.Create(self.DomainScene(), unitConfig_2.Id, UnitType.Shooter);
                unitComponent.Add(unitenemy);
                self.enemylist.Add(unitenemy.Id);
            }
        }
    }
}