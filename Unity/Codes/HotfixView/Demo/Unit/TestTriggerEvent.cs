using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    [FriendClassAttribute(typeof(ET.Unit))]
    public class TestTriggerEvent : AEvent<EventType.TestEvent>
    {
        protected override void Run(TestEvent args)
        {
            Debug.LogWarning("11111111111111");
        }
    }
}
