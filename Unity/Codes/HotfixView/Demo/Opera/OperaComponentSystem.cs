using System;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

namespace ET
{
    [ObjectSystem]
    public class OperaComponentAwakeSystem : AwakeSystem<OperaComponent>
    {
        public override void AwakeAsync(OperaComponent self)
        {
            self.mapMask = LayerMask.GetMask("Map");
        }
    }

    [ObjectSystem]
    public class OperaComponentUpdateSystem : UpdateSystem<OperaComponent>
    {
        public override void Update(OperaComponent self)
        {
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                self.Update();
            }
        }
    }

    [FriendClass(typeof(OperaComponent))]
    public static class OperaComponentSystem
    {
        public static async void Update(this OperaComponent self)
        {
            //KeyCode.W
            if (InputHelper.GetMouseButtonDown(0) || InputHelper.GetKeyDown(113))
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

            //// KeyCode.R
            //if (InputHelper.GetKeyDown(114))
            //{
            //    CodeLoader.Instance.LoadLogic();
            //    Game.EventSystem.Add(CodeLoader.Instance.GetHotfixTypes());
            //    Game.EventSystem.Load();
            //    Log.Debug("hot reload success!");
            //}

            //KeyCode.W
            if (InputHelper.GetKeyDown(119))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000, self.mapMask))
                {
                    self.ClickPoint = hit.point;
                    self.ZoneScene().GetComponent<SessionComponent>().Session.Call(new C2M_MeteorsAOE()
                    {
                        X = self.ClickPoint.x,
                        Y = self.ClickPoint.y,
                        Z = self.ClickPoint.z,
                    }).Coroutine();
                }

            }

            //KeyCode.E
            if (InputHelper.GetKeyDown(101))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000, self.mapMask))
                {
                    self.ClickPoint = hit.point;
                    self.ZoneScene().GetComponent<SessionComponent>().Session.Call(new C2M_MeteorsAOE()
                    {
                        X = self.ClickPoint.x,
                        Y = self.ClickPoint.y,
                        Z = self.ClickPoint.z,
                    }).Coroutine();
                }

            }

            //alpha1
            if (InputHelper.GetKeyDown(49))
            {
                var uigame = self.ZoneScene().GetComponent<UIComponent>().Get(UIType.UIGame);
                if (await uigame.GetComponent<UIGameComponent>().Consume(0))
                {
                    self.ZoneScene().GetComponent<SessionComponent>().Session.Call(new C2M_HuiXue()).Coroutine();
                }
            }
            //alpha2
            if (InputHelper.GetKeyDown(50))
            {
                var uigame = self.ZoneScene().GetComponent<UIComponent>().Get(UIType.UIGame);
                if (await uigame.GetComponent<UIGameComponent>().Consume(1))
                {

                }
            }
            //alpha3
            if (InputHelper.GetKeyDown(51))
            {
                var uigame = self.ZoneScene().GetComponent<UIComponent>().Get(UIType.UIGame);
                if (await uigame.GetComponent<UIGameComponent>().Consume(2))
                {

                }
            }
            //alpha4
            if (InputHelper.GetKeyDown(52))
            {
                var uigame = self.ZoneScene().GetComponent<UIComponent>().Get(UIType.UIGame);
                if (await uigame.GetComponent<UIGameComponent>().Consume(3))
                {

                }
            }
        }
    }
}