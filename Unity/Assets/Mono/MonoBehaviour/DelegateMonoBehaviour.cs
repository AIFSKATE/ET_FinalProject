using System;
using UnityEngine;

namespace ET
{
    public class DelegateMonoBehaviour : MonoBehaviour
    {
        public long BelongToUnitId;

        public event Action<Collider> on_TriggerEnter;
        public event Action<Collider> on_TriggerStay;
        public event Action<Collider> on_TriggerExit;
        public event Action<Collision> on_CollisionEnter;
        public event Action<Collision> on_CollisionStay;
        public event Action<Collision> on_CollisionExit;
        private void OnTriggerEnter(Collider other)
        {
            on_TriggerEnter?.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            on_TriggerStay?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            on_TriggerExit?.Invoke(other);
        }

        private void OnCollisionEnter(Collision collision)
        {
            on_CollisionEnter?.Invoke(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            on_CollisionStay?.Invoke(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            on_CollisionExit?.Invoke(collision);
        }
        public void Clear()
        {
            BelongToUnitId = 0;
            on_TriggerEnter = null;
            on_TriggerStay = null;
            on_TriggerExit = null;
            on_CollisionEnter = null;
            on_CollisionStay = null;
            on_CollisionExit = null;
        }
    }
}