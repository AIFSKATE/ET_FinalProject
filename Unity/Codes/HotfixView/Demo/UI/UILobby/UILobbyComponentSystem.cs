using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [ObjectSystem]
    public class UILobbyComponentAwakeSystem : AwakeSystem<UILobbyComponent>
    {
        public override void AwakeAsync(UILobbyComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

            self.enterMap = rc.Get<GameObject>("EnterMap");
            self.inputfield = rc.Get<GameObject>("InputField").GetComponent<TMP_InputField>();

            self.inputfield.text = "1";
            self.enterMap.GetComponent<Button>().onClick.AddListener(() => { self.EnterMap().Coroutine(); });
        }
    }

    public static class UILobbyComponentSystem
    {
        public static async ETTask EnterMap(this UILobbyComponent self)
        {
            await EnterMapHelper.EnterMapAsync(self.ZoneScene());
            await UIHelper.Close(self.ZoneScene(), UIType.UILobby);
        }
    }
}