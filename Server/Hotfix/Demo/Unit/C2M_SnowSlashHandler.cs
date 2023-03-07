using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    internal class C2M_SnowSlashHandler : AMActorLocationHandler<Unit, C2M_SnowSlash>
    {
        protected override async ETTask Run(Unit unit, C2M_SnowSlash message)
        {
            MessageHelper.Broadcast(unit, new M2C_SnowSlash() { Id = unit.Id });
            await ETTask.CompletedTask;
        }
    }
}

