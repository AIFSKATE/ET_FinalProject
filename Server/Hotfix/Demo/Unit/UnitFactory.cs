using System;
using UnityEngine;

namespace ET
{
    public static class UnitFactory
    {
        public static Unit Create(Scene scene, long id, UnitType unitType)
        {
            UnitComponent unitComponent = scene.GetComponent<UnitComponent>();
            switch (unitType)
            {
                case UnitType.Player:
                    {
                        Unit unit = unitComponent.AddChildWithId<Unit, int>(id, 1001);
                        //ChildType测试代码 取消注释 编译Server.hotfix 可发现报错
                        //unitComponent.AddChild<Player, string>("Player");
                        unit.AddComponent<MoveComponent>();

                        //这里只是gate网关初始化的一个地址，等到传送到map服务器后，还会再次赋值
                        unit.Position = new Vector3(-10, 0, -10);

                        NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
                        numericComponent.Set(NumericType.Speed, 6f); // 速度是6米每秒
                        numericComponent.Set(NumericType.AOI, 15000); // 视野15米

                        unitComponent.Add(unit);
                        // 加入aoi
                        unit.AddComponent<AOIEntity, int, Vector3>(9 * 1000, unit.Position);
                        return unit;
                    }
                case UnitType.Monster:
                    {
                        UnitConfig unitConfig = UnitConfigCategory.Instance.Get(1002);
                        Unit unit = unitComponent.AddChild<Unit, int>(1002);
                        unit.AddComponent<MoveComponent>();

                        unit.AddComponent<PathfindingComponent, string>(scene.Name);
                        unit.Position = PositionComponent.Instance.NextPosition();
                        //unit.Position = new Vector3(0, 15, 0);

                        //unit.AddComponent<MailBoxComponent>();

                        NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
                        numericComponent.Set(NumericType.Speed, (float)unitConfig.Speed / 10); // 速度是2米每秒
                        numericComponent.Set(NumericType.AOI, 15000); // 视野15米

                        unitComponent.Add(unit);
                        unit.AddComponent<AIComponent, int, int>(2, 100);
                        // 加入aoi
                        unit.AddComponent<AOIEntity, int, Vector3>(9 * 1000, unit.Position);
                        return unit;
                    }
                case UnitType.Shooter:
                    {
                        UnitConfig unitConfig = UnitConfigCategory.Instance.Get(1003);
                        Unit unit = unitComponent.AddChild<Unit, int>(1003);
                        unit.AddComponent<MoveComponent>();

                        unit.AddComponent<PathfindingComponent, string>(scene.Name);
                        unit.Position = PositionComponent.Instance.NextPosition();
                        //unit.Position = new Vector3(0, 15, 0);

                        //unit.AddComponent<MailBoxComponent>();

                        NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
                        numericComponent.Set(NumericType.Speed, (float)unitConfig.Speed / 10); // 速度是2米每秒
                        numericComponent.Set(NumericType.AOI, 15000); // 视野15米

                        unitComponent.Add(unit);
                        unit.AddComponent<AIComponent, int, int>(3, 100);
                        // 加入aoi
                        unit.AddComponent<AOIEntity, int, Vector3>(9 * 1000, unit.Position);
                        return unit;
                    }
                default:
                    throw new Exception($"not such unit type: {unitType}");
            }
        }
    }
}