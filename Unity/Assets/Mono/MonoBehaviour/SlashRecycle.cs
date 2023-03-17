using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class SlashRecycle : MonoBehaviour, IRecycle
    {
        public void Recycle()
        {
            //gameObject.SetActive(false);
        }

        public void Reuse()
        {
            //gameObject.SetActive(true);
        }
    }
}
