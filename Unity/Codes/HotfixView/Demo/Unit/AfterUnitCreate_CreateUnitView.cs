using UnityEngine;

namespace ET
{
    [FriendClassAttribute(typeof(ET.Unit))]
    public class AfterUnitCreate_CreateUnitView : AEvent<EventType.AfterUnitCreate>
    {
        protected override void Run(EventType.AfterUnitCreate args)
        {
            // Unit View层
            // 这里可以改成异步加载，demo就不搞了
            switch (args.Unit.ConfigId)
            {
                case
                    1001:
                    {
                        GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset("Unit.unity3d", "Unit");
                        GameObject prefab = bundleGameObject.Get<GameObject>("Skeleton");

                        GameObject go = UnityEngine.Object.Instantiate(prefab, GlobalComponent.Instance.Unit, true);
                        go.transform.position = args.Unit.Position;
                        args.Unit.AddComponent<GameObjectComponent, GameObject>(go);
                        args.Unit.AddComponent<AnimatorComponent>();
                        args.Unit.AddComponent<SkeletonMonoComponent>();
                        return;
                    }
                case 1002:
                    {
                        GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset("Unit.unity3d", "Unit");
                        GameObject prefab = bundleGameObject.Get<GameObject>("Enemy1");
                        GameObject go = UnityEngine.Object.Instantiate(prefab, GlobalComponent.Instance.Unit, true);
                        go.transform.position = args.Unit.Position;
                        args.Unit.AddComponent<GameObjectComponent, GameObject>(go);
                        args.Unit.AddComponent<AnimatorComponent>();
                        args.Unit.AddComponent<TriggerComponent>();
                        args.Unit.AddComponent<HPComponent>();
                        return;
                    }
            }

        }
    }
}