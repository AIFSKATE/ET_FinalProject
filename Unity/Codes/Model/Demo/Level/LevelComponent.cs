using System.Collections.Generic;

namespace ET
{
    //Client
    [ComponentOf(typeof(Scene))]
    public class LevelComponent : Entity, IAwake, IDestroy
    {
        public int nowlevel;
        public int endlevel;
        public int enemynum;

        public HashSet<long> enemyunit;
    }
}