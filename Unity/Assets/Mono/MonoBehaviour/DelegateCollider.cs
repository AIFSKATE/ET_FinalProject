using System;
using UnityEngine;

namespace ET
{
    public class DelegateCollider : MonoBehaviour
    {
        public long BelongToUnitId;
        
        public Action<Collider> on_TriggerEnter;
        public Action<Collider> on_TriggerStay;
        public Action<Collider> on_TriggerExit;
        public Action<Collision> on_CollisionEnter;
        public Action<Collision> on_CollisionStay;
        public Action<Collision> on_CollisionExit;
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
    }
}