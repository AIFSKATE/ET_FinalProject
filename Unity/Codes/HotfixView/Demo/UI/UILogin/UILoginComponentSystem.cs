using System;
using System.Net;

using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [ObjectSystem]
    public class UILoginComponentAwakeSystem : AwakeSystem<UILoginComponent>
    {
        public override void AwakeAsync(UILoginComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            self.loginBtn = rc.Get<GameObject>("LoginBtn");

            self.loginBtn.GetComponent<Button>().onClick.AddListener(() => { self.OnLogin(); });
            self.account = rc.Get<GameObject>("Account");
            self.password = rc.Get<GameObject>("Password");
        }
    }

    [FriendClass(typeof(UILoginComponent))]
    public static class UILoginComponentSystem
    {
        public static void OnLogin(this UILoginComponent self)
        {
            LoginHelper.Login(
                self.DomainScene(),
                ConstValue.LoginAddress,
                self.account.GetComponent<InputField>().text,
                self.password.GetComponent<InputField>().text).Coroutine();
        }

        public static async void OnTest(this UILoginComponent self)
        {
            var cube = await AddressablesMgrComponent.Instance.LoadAssetAsync<UnityEngine.GameObject>("Cube");
            Log.Debug(cube.name);
            UnityEngine.GameObject.Instantiate(cube);
        }
    }
}
