namespace ET
{
    //Server
    [ObjectSystem]
    public class LevelComponentAwakeSystem : AwakeSystem<LevelComponent>
    {
        public override void AwakeAsync(LevelComponent self)
        {
            self.endlevel = 2;
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
        public static void NextLevel(this LevelComponent self)
        {
            self.nowlevel += 1;
            self.StartLevel(self.nowlevel);
        }

        public static void StartLevel(this LevelComponent self, int nowlevel)
        {
            UnitComponent unitComponent = self.DomainScene().GetComponent<UnitComponent>();
            UnitConfig unitConfig = UnitConfigCategory.Instance.Get(1002);
            for (int i = 0; i < 4; i++)
            {
                Unit unitenemy = UnitFactory.Create(self.DomainScene(), unitConfig.Id, UnitType.Monster);
                unitComponent.Add(unitenemy);
            }
        }
    }
}