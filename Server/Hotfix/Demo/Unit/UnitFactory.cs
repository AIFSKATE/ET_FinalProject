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
                        Unit unit = unitComponent.AddChild<Unit, int>((int)id);
                        unit.AddComponent<MoveComponent>();

                        unit.AddComponent<PathfindingComponent, string>(scene.Name);
                        unit.Position = PositionComponent.Instance.NextPosition();

                        unit.AddComponent<MailBoxComponent>();

                        NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
                        numericComponent.Set(NumericType.Speed, 2f); // 速度是2米每秒
                        numericComponent.Set(NumericType.AOI, 15000); // 视野15米

                        unitComponent.Add(unit);
                        unit.AddComponent<AIComponent, int, int>(2, 1000);
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