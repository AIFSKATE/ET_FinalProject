using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ET
{
    public class Test : MonoBehaviour
    {
        public float t;

        // Update is called once per frame
        void Update()
        {
            t += Time.deltaTime;
            GetComponent<TextMeshProUGUI>().text = "" + (int)t;
        }
    }
}
