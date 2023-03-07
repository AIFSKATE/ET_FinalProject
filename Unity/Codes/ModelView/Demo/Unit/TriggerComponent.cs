using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class TriggerComponent : Entity, IAwake, IDestroy
    {
        public BoxCollider boxCollider { get; set; }
    }
}