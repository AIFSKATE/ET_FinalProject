using System;
using System.IO;
using System.Net;

using UnityEngine;
using UnityEngine.UI;

namespace ET
{

    [ObjectSystem]
    public class PHashComponentAwakeSystem : AwakeSystem<PHashComponent>
    {
        public override void AwakeAsync(PHashComponent self)
        {

        }
    }

    [FriendClass(typeof(UIDrawComponent))]
    public static class PHashComponentSystem
    {
        //压缩图片
        public static Texture2D ReduceSize(this PHashComponent self, Texture tex, int size)
        {
            if (tex == null || size <= 0)
            {
                Debug.Log("图片错误");
                return null;
            }
            Texture2D newTexture = new Texture2D(size, size, TextureFormat.ARGB32, false);
            RenderTexture newTexture_t = RenderTexture.GetTemporary(size, size);
            newTexture_t.filterMode = FilterMode.Bilinear;
            Graphics.Blit(tex, newTexture_t);
            var tpic = RenderTexture.active;
            RenderTexture.active = newTexture_t;
            newTexture.ReadPixels(new Rect(0, 0, size, size), 0, 0);
            newTexture.Apply();
            RenderTexture.active = tpic;
            RenderTexture.ReleaseTemporary(newTexture_t);
            return newTexture;
        }

        //转灰度
        public static Texture2D Tex2Gray(this PHashComponent self, Texture2D tex)
        {
            Color color;
            for (int i = 0; i < tex.height; i++)
            {
                for (int j = 0; j < tex.width; j++)
                {
                    color = tex.GetPixel(j, i);
                    float gray = (color.r * 30 + color.b * 59 + color.b * 11) / 100;
                    tex.SetPixel(j, i, new Color(gray, gray, gray));
                }
            }
            tex.Apply();
            return tex;
        }

        //图片转矩阵
        public static float[,] image2F(this PHashComponent self, Texture2D tex)
        {
            int size = tex.width;
            float[,] f = new float[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    f[i, j] = tex.GetPixel(i, j).r;
                }
            }
            return f;
        }

        //计算DCT矩阵
        public static float[,] creatDCTMatrix(this PHashComponent self, int size)
        {
            //Debug.Log((float)Mathf.Cos(Mathf.PI));
            float[,] ret = new float[size, size];
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    float angle = ((y + 0.5f) * Mathf.PI * x / size);
                    ret[x, y] = cfunc(x, size) * (float)Mathf.Cos(angle);
                }
            }
            return ret;
        }
        static float cfunc(int n, int size)
        {
            if (n == 0)
            {
                return Mathf.Sqrt(1f / size);
            }
            else
            {
                return Mathf.Sqrt(2f / size);
            }

        }

        //矩阵转置
        public static float[,] Transpose(this PHashComponent self, float[,] C)
        {
            int size = C.GetLength(0);
            float[,] ret = new float[size, size];
            for (var x = 0; x < size; x++)
            {
                for (var y = 0; y < size; y++)
                {
                    ret[y, x] = C[x, y];
                }
            }
            return ret;
        }

        //矩阵相乘
        public static float[,] Multiply(this PHashComponent self, float[,] C1, float[,] C2)
        {
            int size = C1.GetLength(0);
            float[,] ret = new float[size, size];
            for (var y = 0; y < size; y++)
            {
                for (var x = 0; x < size; x++)
                {
                    float sum1 = 0;
                    for (int k = 0; k < size; k++)
                    {
                        sum1 += C1[x, k] * C2[k, y];
                    }
                    ret[x, y] = sum1;
                }
            }
            return ret;
        }

        public static float[,] UniversalMultiply(this PHashComponent self, float[,] C1, float[,] C2)
        {
            if (C1.GetLength(1) != C2.GetLength(0))
            {
                throw new ArgumentException();
            }
            int m = C1.GetLength(0);
            int n = C1.GetLength(1);
            int k = C2.GetLength(1);
            float[,] ret = new float[m, k];
            for (int x = 0; x < m; x++)
            {
                for (int z = 0; z < k; z++)
                {
                    for (int y = 0; y < n; y++)
                    {
                        ret[x, z] = ret[x, z] + C1[x, y] * C2[y, z];
                    }
                }
            }
            return ret;
        }

        //DCT均值
        public static float averageDCT(this PHashComponent self, float[,] dct)
        {
            int size = dct.GetLength(0);
            float aver = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    aver += dct[i, j];
                }
            }
            return aver / (size * size);
        }

        //获取当前图片pHash值
        public static string getHash(this PHashComponent self, float[,] dct, float aver)
        {
            string hash = string.Empty;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    hash += (dct[i, j] >= aver ? "1" : "0");
                }
            }
            return hash;
        }

        //计算两图片哈希值的汉明距离
        public static float computeDistance(this PHashComponent self, string hash1, string hash2)
        {
            float dis = 0;
            for (int i = 0; i < hash1.Length; i++)
            {
                if (hash1[i] == hash2[i])
                {
                    dis++;
                }
            }
            Debug.Log(dis + "   " + hash1.Length);
            return dis / hash1.Length;
        }

        public static Texture2D RenderTexture2Texture2D(this PHashComponent self, RenderTexture renderTexture)
        {
            var tpic = RenderTexture.active;
            RenderTexture.active = renderTexture;
            Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            tex.Apply();
            RenderTexture.active = tpic;
            return tex;
        }

        public static Texture2D Texture2Texture2D(this PHashComponent self, Texture texture)
        {
            RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height);
            Graphics.Blit(texture, renderTexture);
            var tex = self.RenderTexture2Texture2D(renderTexture);
            RenderTexture.ReleaseTemporary(renderTexture);
            return tex;
        }
    }
}
