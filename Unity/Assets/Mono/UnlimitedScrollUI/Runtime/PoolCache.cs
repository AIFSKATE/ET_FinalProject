using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnlimitedScrollUI
{
    public struct PoolCacheItem<T>
    {
        public T key;
        public GameObject value;
    }
    internal class PoolCache<T>
    {
        private LinkedList<PoolCacheItem<T>> linkedlist = new LinkedList<PoolCacheItem<T>>();
        private Dictionary<T, LinkedListNode<PoolCacheItem<T>>> cache = new Dictionary<T, LinkedListNode<PoolCacheItem<T>>>();

        public bool TryGet(T tk, out GameObject item)
        {
            if (cache.ContainsKey(tk))
            {
                item = cache[tk].Value.value;
                linkedlist.Remove(cache[tk]);
                cache.Remove(tk);
                return true;
            }
            item = default;
            return false;
        }
        public bool Get(out GameObject item)
        {
            if (linkedlist.Count > 0)
            {
                item = linkedlist.First.Value.value;
                cache.Remove(linkedlist.First.Value.key);
                linkedlist.RemoveFirst();
                return true;
            }
            item = default;
            return false;
        }

        public void Add(T index, GameObject item)
        {
            var t = new PoolCacheItem<T>
            {
                key = index,
                value = item
            };
            var node = linkedlist.AddLast(t);
            cache.Add(t.key, node);
        }

        public void Clear()
        {
            linkedlist.Clear();
            cache.Clear();
        }
    }
}
