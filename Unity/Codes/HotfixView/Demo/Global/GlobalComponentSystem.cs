using UnityEngine;

namespace ET
{
    [ObjectSystem]
    public class GlobalComponentAwakeSystem : AwakeSystem<GlobalComponent>
    {
        public override void AwakeAsync(GlobalComponent self)
        {
            GlobalComponent.Instance = self;

            self.Global = GameObject.Find("/Global").transform;
            self.Unit = GameObject.Find("/Global/Unit").transform;
            self.UI = GameObject.Find("/Global/UI").transform;
            self.Pool = GameObject.Find("/Global/Pool").transform;
        }
    }
}