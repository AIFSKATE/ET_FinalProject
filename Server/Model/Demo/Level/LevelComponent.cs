using System.Collections.Generic;

namespace ET
{
    //Server
    [ComponentOf(typeof(Scene))]
    public class LevelComponent : Entity, IAwake, IDestroy
    {
        public int nowlevel;
        public int endlevel;
        public int playernum;
        public List<long> enemylist;
    }
}