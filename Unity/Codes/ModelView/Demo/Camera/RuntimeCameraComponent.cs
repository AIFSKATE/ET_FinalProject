using Cinemachine;
using DG.Tweening;
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
    public class RuntimeCameraComponent : Entity, IAwake<Transform>, IDestroy
    {
        public static RuntimeCameraComponent Instance { get; set; }

        public Camera camera;

        public CinemachineVirtualCamera cinemachine;

        public Vector3 islanddistance;

        public float time;
    }

    [FriendClass(typeof(RuntimeCameraComponent))]
    public static class RuntimeCameraComponentSystem
    {
        [ObjectSystem]
        public class RuntimeCameraComponentAwakeSystem : AwakeSystem<RuntimeCameraComponent, Transform>
        {
            public override void Awake(RuntimeCameraComponent self, Transform transform)
            {
                self.camera = Camera.main;
                self.cinemachine = self.camera.GetComponent<ReferenceCollector>().Get<GameObject>("CMcam1").GetComponent<CinemachineVirtualCamera>();
                self.SetTarget(transform);
            }
        }


        [ObjectSystem]
        public class RuntimeCameraComponentDestroySystem : DestroySystem<RuntimeCameraComponent>
        {
            public override void Destroy(RuntimeCameraComponent self)
            {
                RuntimeCameraComponent.Instance = null;
            }
        }

        public static void SetTarget(this RuntimeCameraComponent self, Transform transform)
        {
            var transposer = self.cinemachine.AddCinemachineComponent<CinemachineTransposer>(); //添加Transposer组件，用于目标跟随
            var composer = self.cinemachine.AddCinemachineComponent<CinemachineComposer>(); //添加Composer组件，用于看向目标
            transposer.m_FollowOffset = new Vector3(0, 6.2f, 2); //设置跟随偏移
            transposer.m_BindingMode = BindingMode.WorldSpace;
            transposer.m_XDamping = 0;
            transposer.m_YDamping = 0;
            transposer.m_ZDamping = 0;
            transposer.m_YawDamping = 0;
            composer.m_DeadZoneHeight = 0.05f;
            composer.m_DeadZoneWidth = 0.05f;
            self.cinemachine.Follow = transform;
            self.cinemachine.LookAt = transform;
        }

    }
}

