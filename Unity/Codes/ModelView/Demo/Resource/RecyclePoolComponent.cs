using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;

namespace ET
{

    [ComponentOf(typeof(Scene))]
    public class RecyclePoolComponent : Entity, IAwake, IDestroy
    {
        public static RecyclePoolComponent Instance { get; set; }
        public Dictionary<string, Queue<GameObject>> pool;
        //public GameObject poolobj;
    }

    [FriendClass(typeof(RecyclePoolComponent))]
    public static class RecyclePoolComponentSystem
    {
        public class RecyclePoolComponentAwakeSystem : AwakeSystem<RecyclePoolComponent>
        {
            public override void Awake(RecyclePoolComponent self)
            {
                RecyclePoolComponent.Instance = self;
                self.pool = new Dictionary<string, Queue<GameObject>>();
                //self.poolobj = new GameObject();
                //self.poolobj.SetActive(false);
            }
        }

        public class RecyclePoolComponentDestroySystem : DestroySystem<RecyclePoolComponent>
        {
            public override void Destroy(RecyclePoolComponent self)
            {
                foreach (var item in self.pool)
                {
                    item.Value.Clear();
                }
                self.pool.Clear();
                self.pool = null;
                RecyclePoolComponent.Instance = null;
            }
        }

        public static GameObject Get(this RecyclePoolComponent self, string name)
        {
            GameObject gameobjectdata;
            if (!self.pool.ContainsKey(name))
            {
                self.pool[name] = new Queue<GameObject>();
            }
            if (self.pool[name].Count == 0)
            {
                GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset("Unit.unity3d", "Unit");
                try
                {
                    gameobjectdata = bundleGameObject.GetComponent<ReferenceCollector>().Get<GameObject>(name);
                }
                catch (Exception e)
                {
                    throw new Exception($"获取{bundleGameObject.name}的ReferenceCollector key失败, key: {name}", e);
                }
                GameObject mygameobject = GameObject.Instantiate(gameobjectdata);
                mygameobject.name = name;
                self.pool[name].Enqueue(mygameobject);
            }
            var item = self.pool[name].Dequeue();
            item.gameObject.SetActive(true);
            IRecycle recycle = item.GetComponent<IRecycle>();
            recycle?.Reuse();
            return item;
        }

        public static void Recycle(this RecyclePoolComponent self, GameObject item)
        {
            if (!self.pool.ContainsKey(item.name))
            {
                self.pool[item.name] = new Queue<GameObject>();
            }
            IRecycle recycle = item.GetComponent<IRecycle>();
            recycle?.Recycle();
            item.gameObject.SetActive(false);
            self.pool[item.name].Enqueue(item);
        }
    }
}
