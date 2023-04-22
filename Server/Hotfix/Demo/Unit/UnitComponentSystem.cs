using System;
using System.Collections.Generic;

namespace ET
{
    public class UnitComponentAwakeSystem : AwakeSystem<UnitComponent>
    {
        public override void AwakeAsync(UnitComponent self)
        {
            self.playerlist = new List<Unit>();
        }
    }

    public class UnitComponentDestroySystem : DestroySystem<UnitComponent>
    {
        public override void Destroy(UnitComponent self)
        {
        }
    }
    [FriendClass(typeof(ET.UnitComponent))]
    public static class UnitComponentSystem
    {
        public static void Add(this UnitComponent self, Unit unit)
        {
            if (unit.Type == UnitType.Player)
            {
                self.playerlist.Add(unit);
            }
        }

        public static List<Unit> GetPlauerList(this UnitComponent self)
        {
            return self.playerlist;
        }

        public static Unit Get(this UnitComponent self, long id)
        {
            Unit unit = self.GetChild<Unit>(id);
            return unit;
        }

        public static void Remove(this UnitComponent self, long id)
        {
            Unit unit = self.GetChild<Unit>(id);
            unit?.Dispose();
        }
    }
}