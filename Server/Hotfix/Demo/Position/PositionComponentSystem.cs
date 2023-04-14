using System.Resources;
using System;
using UnityEngine;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace ET
{
    [FriendClass(typeof(PositionComponent))]
    public static class PositionComponentSystem
    {
        public class PositionComponentAwakeSystem : AwakeSystem<PositionComponent>
        {
            public override void AwakeAsync(PositionComponent self)
            {
                PositionComponent.Instance = self;
                self.nowindex = 0;
                self.position = new Vector3(0, 15, 0);
                Log.Debug("成功进入PositionComponent");
            }
        }

        public class PositionComponentDestroySystem : DestroySystem<PositionComponent>
        {
            public override void Destroy(PositionComponent self)
            {
                PositionComponent.Instance = null;
            }
        }

        public static Vector3 NextPosition(this PositionComponent self)
        {
            switch (self.nowindex)
            {
                case 0:
                    {
                        self.position.x = 6;
                        self.position.z = 6.5f;
                    }
                    break;
                case 1:
                    {
                        self.position.x = 6;
                        self.position.z = -6.5f;
                    }
                    break;
                case 2:
                    {
                        self.position.x = -5;
                        self.position.z = 6.5f;
                    }
                    break;
                case 3:
                    {
                        self.position.x = -5;
                        self.position.z = -6.5f;
                    }
                    break;
            }
            self.nowindex++;
            Log.Debug("我看见的"+self.nowindex.ToString());
            self.nowindex %= 4;
            return self.position;
        }
    }
}
