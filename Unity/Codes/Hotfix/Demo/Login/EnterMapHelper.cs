using System;


namespace ET
{
    public static class EnterMapHelper
    {
        public static async ETTask EnterMapAsync(Scene zoneScene, int roomid)
        {
            try
            {
                G2C_EnterMap g2CEnterMap = await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2G_EnterMap() { RoomId = roomid }) as G2C_EnterMap;
                zoneScene.GetComponent<PlayerComponent>().MyId = g2CEnterMap.MyId;

                // 等待场景切换完成
                await zoneScene.GetComponent<ObjectWait>().Wait<WaitType.Wait_SceneChangeFinish>();

                Game.EventSystem.Publish(new EventType.EnterMapFinish() { ZoneScene = zoneScene });
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}