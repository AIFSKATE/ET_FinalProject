using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnlimitedScrollUI
{

    internal class PoolCache
    {
        private LinkedList<GameObject> linkedlist = new LinkedList<GameObject>();

        public bool TryGet(out GameObject item)
        {
            if (linkedlist.Count > 0)
            {
                item = linkedlist.First.Value;
                linkedlist.RemoveFirst();
                return true;
            }
            item = default;
            return false;
        }

        public void Add(GameObject item)
        {
            linkedlist.AddLast(item);
        }

        public void Clear()
        {
            linkedlist.Clear();
        }
    }
}
