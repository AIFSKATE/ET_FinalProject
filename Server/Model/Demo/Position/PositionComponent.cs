using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class PositionComponent : Entity, IAwake, IDestroy
    {
        public static PositionComponent Instance { get; set; }

        public int nowindex;

        public Vector3 position;

        public List<int> list;
    }
}