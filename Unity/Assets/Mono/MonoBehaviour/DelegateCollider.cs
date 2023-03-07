using UnityEngine;

namespace ET
{
    public class DelegateCollider : MonoBehaviour
    {
        public long BelongToUnitId;

        public delegate void On_Trigger(Collider other);
        public delegate void On_Collision(Collision other);
        public On_Trigger on_TriggerEnter;
        public On_Trigger on_TriggerStay;
        public On_Trigger on_TriggerExit;
        public On_Collision on_CollisionEnter;
        public On_Collision on_CollisionStay;
        public On_Collision on_CollisionExit;
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