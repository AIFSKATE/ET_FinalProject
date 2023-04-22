using ET.EventType;
using System.Collections.Generic;

namespace ET
{
    //Client
    [ObjectSystem]
    public class LevelComponentAwakeSystem : AwakeSystem<LevelComponent>
    {
        public override void AwakeAsync(LevelComponent self)
        {
            self.nowlevel = 1;
            self.enemyunit = new System.Collections.Generic.HashSet<long>();
        }
    }

    [ObjectSystem]
    public class LevelComponentDestroySystem : DestroySystem<LevelComponent>
    {
        public override void Destroy(LevelComponent self)
        {
            self.enemyunit.Clear();
        }
    }

    [FriendClass(typeof(ET.LevelComponent))]
    public static class LevelComponentSystem
    {
        public static void AddEnemy(this LevelComponent self, Unit enemy)
        {
            self.enemyunit.Add(enemy.Id);
        }
        public static void RemoveEnemy(this LevelComponent self, Unit enemy)
        {
            self.enemyunit.Remove(enemy.Id);
            if (self.enemyunit.Count == 0)
            {
                self.nowlevel++;
                self.StartLevel(self.nowlevel).Coroutine();
            }
        }

        public static HashSet<long> GetAllEnemy(this LevelComponent self)
        {
            return self.enemyunit;
        }


        public static async ETTask StandingBy(this LevelComponent self)
        {
            //这一块是关卡加载通知
            M2C_Standingby m2C_Standingby = await self.ZoneScene().GetComponent<SessionComponent>().Session.Call(new C2M_Standingby()) as M2C_Standingby;

            if (m2C_Standingby.Error == ErrorCode.ERR_Success)
            {
                self.StartLevel(self.nowlevel).Coroutine();
                self.endlevel = m2C_Standingby.endlevel;
            }
        }

        public static async ETTask StartLevel(this LevelComponent self, int nowlevel)
        {
            M2C_Startlevel m2C_Startlevel = await self.ZoneScene().GetComponent<SessionComponent>().Session.Call(new C2M_Startlevel() { nowlevel = nowlevel }) as M2C_Startlevel;
            if (m2C_Startlevel.Error == ErrorCode.ERR_LevelEND)
            {
                self.EndLevel().Coroutine();
            }
        }

        public static async ETTask EndLevel(this LevelComponent self)
        {
            Game.EventSystem.Publish(new EndLevel() { ZoneScene = self.ZoneScene(), time = 1 });
            await ETTask.CompletedTask;
        }
    }
}