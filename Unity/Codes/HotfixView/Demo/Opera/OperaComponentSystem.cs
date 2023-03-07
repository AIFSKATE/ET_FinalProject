using System;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

namespace ET
{
    [ObjectSystem]
    public class OperaComponentAwakeSystem : AwakeSystem<OperaComponent>
    {
        public override void Awake(OperaComponent self)
        {
            self.mapMask = LayerMask.GetMask("Map");
        }
    }

    [ObjectSystem]
    public class OperaComponentUpdateSystem : UpdateSystem<OperaComponent>
    {
        public override void Update(OperaComponent self)
        {
            self.Update();
        }
    }

    [FriendClass(typeof(OperaComponent))]
    public static class OperaComponentSystem
    {
        public static void Update(this OperaComponent self)
        {

            if (InputHelper.GetMouseButtonDown(0))
            {
                //GameObject SnowSlash = RecyclePoolComponent.Instance.Get("SnowSlash");
                //Scene currentScene = self.DomainScene();
                //Scene ZoneScene = currentScene.DomainScene().Parent.ZoneScene();
                //var id = ZoneScene.GetComponent<PlayerComponent>().MyId;
                //UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
                //var myunit = unitComponent.Get(id);
                //var mygameobject = myunit.GetComponent<GameObjectComponent>().GameObject;
                //GameObject.Instantiate(SnowSlash, mygameobject.transform);
                self.ZoneScene().GetComponent<SessionComponent>().Session.Call(new C2M_SnowSlash()).Coroutine();
            }

            if (InputHelper.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000, self.mapMask))
                {
                    self.ClickPoint = hit.point;
                    self.frameClickMap.X = self.ClickPoint.x;
                    self.frameClickMap.Y = self.ClickPoint.y;
                    self.frameClickMap.Z = self.ClickPoint.z;
                    self.ZoneScene().GetComponent<SessionComponent>().Session.Send(self.frameClickMap);
                }
            }

            // KeyCode.R
            if (InputHelper.GetKeyDown(114))
            {
                CodeLoader.Instance.LoadLogic();
                Game.EventSystem.Add(CodeLoader.Instance.GetHotfixTypes());
                Game.EventSystem.Load();
                Log.Debug("hot reload success!");
            }

            // KeyCode.T
            //if (InputHelper.GetKeyDown(116))
            //{
            //    C2M_TransferMap c2MTransferMap = new C2M_TransferMap();
            //    self.ZoneScene().GetComponent<SessionComponent>().Session.Call(c2MTransferMap).Coroutine();
            //}
        }
    }
}