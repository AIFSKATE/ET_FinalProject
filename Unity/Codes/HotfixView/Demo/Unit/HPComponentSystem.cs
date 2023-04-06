using System;
using UnityEngine;

namespace ET
{
    public static class HPComponentSystem
    {

        [ObjectSystem]
        public class AwakeSystem : AwakeSystem<HPComponent>
        {
            public override void Awake(HPComponent self)
            {
                self.uihp = self.ZoneScene().GetComponent<UIComponent>().Get(UIType.UIHP);
                self.gameObject = self.Parent.GetComponent<GameObjectComponent>().GameObject;
                self.uihp.GetComponent<UIHPComponent>().InitHP(self.Parent as Unit);
            }
        }

        [ObjectSystem]
        public class UpdateSystem : UpdateSystem<HPComponent>
        {
            public override void Update(HPComponent self)
            {
                self.uihp.GetComponent<UIHPComponent>().SetHP(self.gameObject, self.Parent as Unit);
            }
        }

    }
}