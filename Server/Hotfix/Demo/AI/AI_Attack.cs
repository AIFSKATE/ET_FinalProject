using UnityEngine;

namespace ET
{
    public class AI_Attack : AAIHandler
    {
        public override int Check(AIComponent aiComponent, AIConfig aiConfig)
        {
            var unitlist = aiComponent.DomainScene().GetComponent<UnitComponent>().Children;
            Unit unit = aiComponent.Parent as Unit;
            foreach (var item in unitlist)
            {
                var temp = item.Value as Unit;
                if (temp.Type == UnitType.Player)
                {
                    unit = temp;
                    break;
                }
            }
            UnityEngine.Vector3 targetposition = unit.Position;
            UnityEngine.Vector3 myposition = (aiComponent.Parent as Unit).Position;
            if (Vector3.Distance(targetposition, myposition) <= 5)
            {
                Log.Debug("你离得很近了！！\n");
                return 0;
            }
            return 1;
        }

        public override async ETTask Execute(AIComponent aiComponent, AIConfig aiConfig, ETCancellationToken cancellationToken)
        {
            //Scene zoneScene = aiComponent.DomainScene();

            //Unit myUnit = aiComponent.Parent as Unit;
            //if (myUnit == null)
            //{
            //    return;
            //}

            //Log.Debug("开始攻击");

            //for (int i = 0; i < 100000; ++i)
            //{
            //    Log.Debug($"攻击: {i}次");

            //    // 因为协程可能被中断，任何协程都要传入cancellationToken，判断如果是中断则要返回
            //    bool timeRet = await TimerComponent.Instance.WaitAsync(1000, cancellationToken);
            //    if (!timeRet)
            //    {
            //        return;
            //    }
            //}
            await ETTask.CompletedTask;
        }
    }
}