namespace ET
{
    //Server
    [ObjectSystem]
    public class LevelComponentAwakeSystem : AwakeSystem<LevelComponent>
    {
        public override void AwakeAsync(LevelComponent self)
        {
            self.endlevel = 1;
        }
    }
    [FriendClass(typeof(ET.LevelComponent))]
    public static class LevelComponentSystem
    {
        public static int GetEndLevel(this LevelComponent self)
        {
            return self.endlevel;
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