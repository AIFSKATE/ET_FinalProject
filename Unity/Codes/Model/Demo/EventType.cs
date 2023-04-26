using UnityEngine;

namespace ET
{
    namespace EventType
    {
        public struct AppStart
        {
        }


        public struct SceneChangeStart
        {
            public Scene ZoneScene;
        }

        public struct SceneChangeFinish
        {
            public Scene ZoneScene;
            public Scene CurrentScene;
        }

        public class ChangePosition : DisposeObject
        {
            public static readonly ChangePosition Instance = new ChangePosition();

            public Unit Unit;
            public WrapVector3 OldPos = new WrapVector3();

            // 因为是重复利用的，所以用完PublishClass会调用Dispose
            public override void Dispose()
            {
                this.Unit = null;
            }
        }

        public class ChangeRotation : DisposeObject
        {
            public static readonly ChangeRotation Instance = new ChangeRotation();

            public Unit Unit;

            // 因为是重复利用的，所以用完PublishClass会调用Dispose
            public override void Dispose()
            {
                this.Unit = null;
            }
        }

        public struct PingChange
        {
            public Scene ZoneScene;
            public long Ping;
        }

        public struct AfterCreateZoneScene
        {
            public Scene ZoneScene;
        }

        public struct AfterCreateCurrentScene
        {
            public Scene CurrentScene;
        }

        public struct AfterCreateLoginScene
        {
            public Scene LoginScene;
        }

        public struct AppStartInitFinish
        {
            public Scene ZoneScene;
        }

        public struct LoginFinish
        {
            public Scene ZoneScene;
        }

        public struct LoadingBegin
        {
            public Scene Scene;
        }

        public struct LoadingFinish
        {
            public Scene Scene;
        }

        public struct EnterMapFinish
        {
            public Scene ZoneScene;
        }

        public struct AfterUnitCreate
        {
            public Unit Unit;
        }

        public struct MoveStart
        {
            public Unit Unit;
        }

        public struct MoveStop
        {
            public Unit Unit;
        }

        public struct SnowSlash
        {
            public Unit Unit;
            public Session session;
        }
        public struct HuiXue
        {
            public Unit Unit;
            public Session session;
        }
        public struct IncreaseMaxHP
        {
            public Unit Unit;
            public Session session;
        }
        public struct IncreaseAttack
        {
            public Unit Unit;
            public Session session;
        }
        public struct IncreaseDefense
        {
            public Unit Unit;
            public Session session;
        }

        public struct MeteorsAOE
        {
            public Unit Unit;
            public Session session;
            public Vector3 Position;
        }

        public struct SpinningChop
        {
            public Unit Unit;
            public Session session;
            public Vector3 Forward;
        }
        public struct DragonPunch
        {
            public Unit Unit;
            public Session session;
            public Vector3 Forward;
        }

        public struct Damage
        {
            public Unit Unit;
            public int damage;
        }

        public struct MonsterDamage
        {
            public Unit PlayerUnit;
            public Unit MonsterUnit;
            public int damage;
            public Session session;
        }
        public struct MonsterShoot
        {
            public Unit PlayerUnit;
            public Unit MonsterUnit;
            public int damage;
            public Session session;
        }

        public struct AnimatorTrigger
        {
            public Unit Unit;
            public Session session;
            public string trigger;
        }

        public struct EndLevel
        {
            public Scene ZoneScene;
            public int time;
        }

        public struct PrepareTheNext
        {
            public Scene ZoneScene;
            public int time;
        }

        public struct TestEvent
        {
            //public Scene zonescene;
        }
    }
}