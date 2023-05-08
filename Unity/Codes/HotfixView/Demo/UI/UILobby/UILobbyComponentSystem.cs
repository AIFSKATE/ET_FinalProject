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
    [FriendClassAttribute(typeof(ET.UILobbyComponent))]
    public static class UILobbyComponentSystem
    {
        public static async ETTask EnterMap(this UILobbyComponent self)
        {
            int.TryParse(self.inputfield.text, out int roomid);
            await EnterMapHelper.EnterMapAsync(self.ZoneScene(), roomid);
            await UIHelper.Close(self.ZoneScene(), UIType.UILobby);
        }
    }
}