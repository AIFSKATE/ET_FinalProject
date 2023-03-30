using System;
using System.IO;
using System.Net;

using UnityEngine;
using UnityEngine.UI;

namespace ET
{

    [ObjectSystem]
    public class DrawComponentAwakeSystem : AwakeSystem<DrawComponent>
    {
        public override void Awake(DrawComponent self)
        {

        }
    }

    [FriendClass(typeof(DrawComponent))]
    public static class DrawComponentSystem
    {
        public static async ETTask Init(this DrawComponent self, Canvas canvas, Camera uiCamera, RawImage rawImage, int size, Color brushColor)
        {
            await self.DomainScene().GetComponent<ResourcesLoaderComponent>().LoadAsync("material.unity3d");
            self.brushMat = (Material)ResourcesComponent.Instance.GetAsset("material.unity3d", "DrawWord");
            self.clearMat = (Material)ResourcesComponent.Instance.GetAsset("material.unity3d", "Clear");

            self.m_uiCamera = uiCamera;
            self.m_renderMode = canvas.renderMode;
            self.rawImage = rawImage;

            self.m_rawImageSizeX = self.rawImage.GetComponent<RectTransform>().rect.width;
            self.m_rawImageSizeY = self.rawImage.GetComponent<RectTransform>().rect.height;

            self.m_renderTex = RenderTexture.GetTemporary(512, 512);
            //m_lastRenderTex = RenderTexture.GetTemporary(512, 512);
            self.rawImage.texture = self.m_renderTex;

            self.brushMat.SetColor("_Color", Color.black);
            self.brushMat.SetFloat("_Size", size);

            self.SetProperty(brushColor, size);
            self.Clear();

            await ETTask.CompletedTask;
        }

        public static void Release(this DrawComponent self)
        {
            if (self.m_renderTex != null) self.m_renderTex.Release();
            //if (m_lastRenderTex != null) m_lastRenderTex.Release();
        }

        public static void SetProperty(this DrawComponent self, Color brushColor, int size)
        {
            self.brushMat.SetColor("_Color", brushColor);
            self.brushMat.SetFloat("_Size", size);
        }
        public static void SetProperty(this DrawComponent self, Color brushColor)
        {
            self.brushMat.SetColor("_Color", brushColor);
        }
        public static void SetProperty(this DrawComponent self, int size)
        {
            self.brushMat.SetFloat("_Size", size);
        }

        public static void Clear(this DrawComponent self)
        {
            Graphics.Blit(null, self.m_renderTex, self.clearMat);
            //Graphics.Blit(null, m_lastRenderTex, clearMat);
        }

        public static void Save(this DrawComponent self)
        {
            var t = RenderTexture.active;
            RenderTexture.active = self.m_renderTex;
            Texture2D png = new Texture2D(self.m_renderTex.width, self.m_renderTex.height, TextureFormat.ARGB32, false);
            png.ReadPixels(new Rect(0, 0, self.m_renderTex.width, self.m_renderTex.height), 0, 0);
            byte[] bytes = png.EncodeToJPG();
            //using (FileStream stream = File.OpenWrite(@"C:\Users\10671\Desktop\毕业设计\temp.jpg"))
            //{
            //    stream.Write(bytes, 0, bytes.Length);
            //    Debug.Log($"file {stream.Name} written");
            //}
            //RenderTexture.active = t;
        }

        public static void StartWrite(this DrawComponent self, Vector3 pos)
        {
            self.m_mousePos = pos;
            self.m_lastMousePos = pos;
        }

        public static void Writing(this DrawComponent self, Vector3 pos)
        {
            self.m_mousePos = pos;
            self.Paint();
            self.m_lastMousePos = pos;
        }

        public static void Paint(this DrawComponent self)
        {
            var uv = self.GetUV(self.m_mousePos);
            var last = self.GetUV(self.m_lastMousePos);

            self.brushMat.SetTexture("_Tex", self.m_renderTex);
            self.brushMat.SetVector("_UV", uv);
            self.brushMat.SetVector("_LastUV", last);

            Graphics.Blit(self.m_renderTex, self.m_renderTex, self.brushMat);
        }

        public static Vector2 GetUV(this DrawComponent self, Vector2 brushPos)
        {
            //获取图片在屏幕中的像素位置
            Vector2 rawImagePos = Vector2.zero;

            //判断所在画布的渲染方式，不同渲染方式的位置计算方式不同
            //switch (m_renderMode)
            //{
            //    case RenderMode.ScreenSpaceOverlay:
            //        rawImagePos = rawImage.rectTransform.position;
            //        break;
            //    default:
            //        rawImagePos = m_uiCamera.WorldToScreenPoint(rawImage.rectTransform.position);
            //        break;
            //}

            //换算鼠标在图片中心点的像素位置
            //Vector2 pos = brushPos - rawImagePos;
            Vector2 pos = Vector2.left;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(self.rawImage.rectTransform, brushPos, null, out pos);
            //换算鼠标在图片中UV坐标
            Vector2 uv = new Vector2(pos.x / self.m_rawImageSizeX, pos.y / self.m_rawImageSizeY);

            return uv;
        }
    }
}
