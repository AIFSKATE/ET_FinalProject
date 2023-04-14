using System;


namespace ET
{
    [FriendClass(typeof(GateMapComponent))]
    [MessageHandler]
    public class C2G_ExitMapHandler : AMRpcHandler<C2G_ExitMap, G2C_ExitMap>
    {
        protected override async ETTask Run(Session session, C2G_ExitMap request, G2C_ExitMap response, Action reply)
        {
            Player player = session.GetComponent<SessionPlayerComponent>().GetMyPlayer();

            player.RemoveComponent<GateMapComponent>();

            reply();
            await ETTask.CompletedTask;
        }
    }
}