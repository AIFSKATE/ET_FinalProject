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
    public class CameraComponent : Entity, IAwake, IDestroy
    {
        public Camera camera;

        public CinemachineVirtualCamera cinemachine;

        public Vector3 islanddistance;

        public Transform island;

        public Vector3 selfposition;

        public Vector3 selfrotation;
    }

    [FriendClass(typeof(CameraComponent))]
    public static class CameraComponentSystem
    {
        [ObjectSystem]
        public class CameraComponentAwakeSystem : AwakeSystem<CameraComponent>
        {
            public override void AwakeAsync(CameraComponent self)
            {
                self.camera = Camera.main;
                self.selfposition = self.camera.GetComponent<Transform>().position;
                self.selfrotation = self.camera.GetComponent<Transform>().rotation.eulerAngles;
                self.cinemachine = self.camera.GetComponent<ReferenceCollector>().Get<GameObject>("CMcam1").GetComponent<CinemachineVirtualCamera>();
                self.island = self.camera.GetComponent<ReferenceCollector>().Get<GameObject>("Ground").transform;
                self.islanddistance = self.camera.transform.position - self.island.position;

            }
        }

        [ObjectSystem]
        public class CameraComponentDestroySystem : DestroySystem<CameraComponent>
        {
            public override void Destroy(CameraComponent self)
            {

            }
        }
    }
}

