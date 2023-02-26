using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class AddressablesMgrComponent : Entity, IAwake, IDestroy
    {
        public static AddressablesMgrComponent Instance { get; set; }

        //有一个容器 帮助我们存储 异步加载的返回值
        public Dictionary<string, AsyncOperationHandle> resDic = new Dictionary<string, AsyncOperationHandle>();
    }

    [FriendClass(typeof(AddressablesMgrComponent))]
    public static class AddressablesMgrComponentSystem
    {
        [ObjectSystem]
        public class AddressablesMgrComponentAwakeSystem : AwakeSystem<AddressablesMgrComponent>
        {
            public override void Awake(AddressablesMgrComponent self)
            {
                AddressablesMgrComponent.Instance = self;
            }
        }

        [ObjectSystem]
        public class AddressablesMgrComponentDestroySystem : DestroySystem<AddressablesMgrComponent>
        {
            public override void Destroy(AddressablesMgrComponent self)
            {
                AddressablesMgrComponent.Instance = null;
                foreach (var item in self.resDic.Values)
                {
                    Addressables.Release(item);
                }
                self.resDic.Clear();
                AssetBundle.UnloadAllAssetBundles(true);
                Resources.UnloadUnusedAssets();
                GC.Collect();
            }
        }

        public static void Release<T>(this AddressablesMgrComponent self, string name)
        {
            //由于存在同名 不同类型资源的区分加载
            //所以我们通过名字和类型拼接作为 key
            string keyName = name + "_" + typeof(T).Name;
            if (self.resDic.ContainsKey(keyName))
            {
                //取出对象 移除资源 并且从字典里面移除
                AsyncOperationHandle<T> handle = self.resDic[keyName].Convert<T>();
                Addressables.Release(handle);
                self.resDic.Remove(keyName);
            }
        }

        public static void Release<T>(this AddressablesMgrComponent self, params string[] keys)
        {
            //1.构建一个keyName  之后用于存入到字典中
            List<string> list = new List<string>(keys);
            string keyName = "";
            foreach (string key in list)
                keyName += key + "_";
            keyName += typeof(T).Name;

            if (self.resDic.ContainsKey(keyName))
            {
                //取出字典里面的对象
                AsyncOperationHandle<IList<T>> handle = self.resDic[keyName].Convert<IList<T>>();
                Addressables.Release(handle);
                self.resDic.Remove(keyName);
            }
        }
        //异步加载单个资源的方法
        public static void LoadAssetAsync<T>(this AddressablesMgrComponent self, string name, Action<AsyncOperationHandle<T>> callBack)
        {
            //由于存在同名 不同类型资源的区分加载
            //所以我们通过名字和类型拼接作为 key
            string keyName = name + "_" + typeof(T).Name;
            AsyncOperationHandle<T> handle;
            //如果已经加载过该资源
            if (self.resDic.ContainsKey(keyName))
            {
                //获取异步加载返回的操作内容
                handle = self.resDic[keyName].Convert<T>();

                //判断 这个异步加载是否结束
                if (handle.IsDone)
                {
                    //如果成功 就不需要异步了 直接相当于同步调用了 这个委托函数 传入对应的返回值
                    callBack(handle);
                }
                //还没有加载完成
                else
                {
                    //如果这个时候 还没有异步加载完成 那么我们只需要 告诉它 完成时做什么就行了
                    handle.Completed += (obj) =>
                    {
                        if (obj.Status == AsyncOperationStatus.Succeeded)
                            callBack(obj);
                    };
                }
                return;
            }

            //如果没有加载过该资源
            //直接进行异步加载 并且记录
            handle = Addressables.LoadAssetAsync<T>(name);
            handle.Completed += (obj) =>
            {
                if (obj.Status == AsyncOperationStatus.Succeeded)
                    callBack(obj);
                else
                {
                    Debug.LogWarning(keyName + "资源加载失败");
                    if (self.resDic.ContainsKey(keyName))
                        self.resDic.Remove(keyName);
                }
            };
            self.resDic.Add(keyName, handle);
        }

        public static async ETTask<T> LoadAssetAsync<T>(this AddressablesMgrComponent self, string name)
        {
            string keyName = name + "_" + typeof(T).Name;
            AsyncOperationHandle<T> handle;
            if (self.resDic.ContainsKey(keyName))
            {
                handle = self.resDic[keyName].Convert<T>();
                if (!handle.IsDone)
                {
                    await handle.Task;
                }
                return handle.Result;
            }
            handle = Addressables.LoadAssetAsync<T>(name);
            await handle.Task;
            self.resDic.Add(keyName, handle);
            return handle.Result;
        }

        //异步加载多个资源 或者 加载指定资源
        public static void LoadAssetAsync<T>(this AddressablesMgrComponent self, Addressables.MergeMode mode, Action<T> callBack, params string[] keys)
        {
            //1.构建一个keyName  之后用于存入到字典中
            List<string> list = new List<string>(keys);
            string keyName = "";
            foreach (string key in list)
                keyName += key + "_";
            keyName += typeof(T).Name;
            //2.判断是否存在已经加载过的内容 
            //存在做什么
            AsyncOperationHandle<IList<T>> handle;
            if (self.resDic.ContainsKey(keyName))
            {
                handle = self.resDic[keyName].Convert<IList<T>>();
                //异步加载是否结束
                if (handle.IsDone)
                {
                    foreach (T item in handle.Result)
                        callBack(item);
                }
                else
                {
                    handle.Completed += (obj) =>
                    {
                        //加载成功才调用外部传入的委托函数
                        if (obj.Status == AsyncOperationStatus.Succeeded)
                        {
                            foreach (T item in handle.Result)
                                callBack(item);
                        }
                    };
                }
                return;
            }
            //不存在做什么
            handle = Addressables.LoadAssetsAsync<T>(list, callBack, mode);
            handle.Completed += (obj) =>
            {
                if (obj.Status == AsyncOperationStatus.Failed)
                {
                    Debug.LogError("资源加载失败" + keyName);
                    if (self.resDic.ContainsKey(keyName))
                        self.resDic.Remove(keyName);
                }
            };
            self.resDic.Add(keyName, handle);
        }
    }
}