using System.Collections.Generic;

namespace ET
{
	[ComponentOf(typeof(Scene))]
	[ChildType(typeof(Unit))]
	public class UnitComponent: Entity, IAwake, IDestroy
	{
		public List<Unit> playerlist;
    }
}