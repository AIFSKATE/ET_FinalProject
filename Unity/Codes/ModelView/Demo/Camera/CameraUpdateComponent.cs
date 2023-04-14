using Cinemachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    [ComponentOf(typeof(CameraComponent))]
    public class CameraUpdateComponent : Entity, IAwake, IUpdate, IDestroy
    {
        public Camera camera;

        public CinemachineVirtualCamera cinemachine;

        public Vector3 islanddistance;

        public Transform island;

        public Vector3 selfposition;

        public Vector3 selfrotation;

        public float time;
    }

    [FriendClass(typeof(CameraComponent))]
    public static class CameraUpdateComponentSystem
    {
        [ObjectSystem]
        [FriendClassAttribute(typeof(ET.CameraComponent))]
        public class CameraComponentAwakeSystem : AwakeSystem<CameraUpdateComponent>
        {
            public override void AwakeAsync(CameraUpdateComponent self)
            {
                self.selfposition = (self.Parent as CameraComponent).selfposition;
                self.selfrotation = (self.Parent as CameraComponent).selfrotation;
                self.camera = (self.Parent as CameraComponent).camera;
                self.cinemachine = (self.Parent as CameraComponent).cinemachine;
                self.island = (self.Parent as CameraComponent).island;
                self.islanddistance = (self.Parent as CameraComponent).islanddistance;

                self.cinemachine.transform.position = self.selfposition;
                self.cinemachine.transform.rotation = Quaternion.Euler(self.selfrotation);

                self.time = 0;
            }
        }

        [ObjectSystem]
        public class CameraComponentUpdateSystem : UpdateSystem<CameraUpdateComponent>
        {
            public override void Update(CameraUpdateComponent self)
            {
                self.time += Time.deltaTime;
                //self.camera.transform.Rotate(Vector3.up, 1, Space.World);
                self.cinemachine.transform.position = self.island.position + Quaternion.AngleAxis(self.time * 5, Vector3.up) * self.islanddistance;
                self.cinemachine.transform.Rotate(Time.deltaTime * 5 * Vector3.up, Space.World);
                //self.camera.transform.LookAt((self.camera.transform.position- self.island.position).normalized);
            }
        }

        [ObjectSystem]
        public class CameraComponentDestroySystem : DestroySystem<CameraUpdateComponent>
        {
            public override void Destroy(CameraUpdateComponent self)
            {

            }
        }
    }
}
