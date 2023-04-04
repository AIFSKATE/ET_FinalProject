using System;
using System.Net;

using UnityEngine;
using UnityEngine.UI;
using UnlimitedScrollUI;

namespace ET
{
    [ObjectSystem]
    public class UIBagComponentAwakeSystem : AwakeSystem<UIBagComponent>
    {
        public override void Awake(UIBagComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            GameObject Cell = rc.Get<GameObject>("Cell");
            self.scrollercomponent = rc.Get<GameObject>("GridUnlimitedScroller").GetComponent<GridUnlimitedScroller>();
            self.scrollercomponent.Generate(Cell, 100, (index, iCell) =>
            {
                var regularCell = iCell as RegularCell;
                if (regularCell != null) regularCell.onGenerated?.Invoke(index);
            });
        }
    }

    [FriendClass(typeof(UIBagComponent))]
    public static class UIBagComponentSystem
    {

    }
}
