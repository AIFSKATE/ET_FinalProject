using Cinemachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using static Cinemachine.CinemachineTransposer;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class CameraComponent : Entity, IAwake, IUpdate, IDestroy
    {
        public static CameraComponent Instance { get; set; }

        public Camera camera;

        public CinemachineVirtualCamera cinemachine;

        public Vector3 islanddistance;

        public Transform island;

        public Quaternion quaternion;

        public float time;
    }

    [FriendClass(typeof(CameraComponent))]
    public static class CameraComponentSystem
    {
        [ObjectSystem]
        public class CameraComponentAwakeSystem : AwakeSystem<CameraComponent>
        {
            public override void Awake(CameraComponent self)
            {
                CameraComponent.Instance = self;
                self.camera = Camera.main;
                self.cinemachine = self.camera.GetComponent<ReferenceCollector>().Get<GameObject>("CMcam1").GetComponent<CinemachineVirtualCamera>();
                self.island = self.camera.GetComponent<ReferenceCollector>().Get<GameObject>("Ground").transform;
                self.islanddistance = self.camera.transform.position - self.island.position;
                self.quaternion = Quaternion.FromToRotation(self.islanddistance.normalized, self.camera.transform.forward);
            }
        }

        [ObjectSystem]
        public class CameraComponentUpdateSystem : UpdateSystem<CameraComponent>
        {
            public override void Update(CameraComponent self)
            {
                self.time += Time.deltaTime;
                //self.camera.transform.Rotate(Vector3.up, 1, Space.World);
                self.cinemachine.transform.position = self.island.position + Quaternion.AngleAxis(self.time * 5, Vector3.up) * self.islanddistance;
                self.cinemachine.transform.Rotate(Time.deltaTime * 5 * Vector3.up, Space.World);
                //self.camera.transform.LookAt((self.camera.transform.position- self.island.position).normalized);
            }
        }

        [ObjectSystem]
        public class CameraComponentDestroySystem : DestroySystem<CameraComponent>
        {
            public override void Destroy(CameraComponent self)
            {
                CameraComponent.Instance = null;
            }
        }

        public static void SetTarget(this CameraComponent self, Transform transform)
        {
            var transposer = self.cinemachine.AddCinemachineComponent<CinemachineTransposer>(); //添加Transposer组件，用于目标跟随
            var composer = self.cinemachine.AddCinemachineComponent<CinemachineHardLookAt>(); //添加Composer组件，用于看向目标
            transposer.m_FollowOffset = new Vector3(0, 4.5f, 2); //设置跟随偏移
            transposer.m_BindingMode = BindingMode.WorldSpace;
            self.cinemachine.Follow = transform;
            self.cinemachine.LookAt = transform;
        }

    }
}

