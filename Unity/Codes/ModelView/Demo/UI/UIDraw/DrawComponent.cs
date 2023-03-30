using System;
using System.Net;

using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [ComponentOf(typeof(UIDrawComponent))]
    public class DrawComponent : Entity, IAwake
    {
        //预设
        public RawImage rawImage;
        public Material brushMat;
        public Material clearMat;

        //传入
        public Camera m_uiCamera;
        public RenderMode m_renderMode;

        public float m_rawImageSizeX;
        public float m_rawImageSizeY;
        public Vector3 m_mousePos;
        public Vector3 m_lastMousePos;
        public RenderTexture m_renderTex;
        //private RenderTexture m_lastRenderTex;
    }
}
