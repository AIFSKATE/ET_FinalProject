using ILRuntime.Runtime;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Security.Policy;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using Color = UnityEngine.Color;

namespace ET
{
    [ObjectSystem]
    public class UIDrawComponentAwakeSystem : AwakeSystem<UIDrawComponent>
    {
        public override void AwakeAsync(UIDrawComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            self.fulu = rc.Get<GameObject>("fulu").GetComponent<Image>();
            self.mydraw = rc.Get<GameObject>("mydraw").GetComponent<RawImage>();
            self.canvasRect = rc.Get<GameObject>("Canvas").GetComponent<RectTransform>();
            self.contentRect = rc.Get<GameObject>("Content").GetComponent<RectTransform>();
            self.checkBtn = rc.Get<GameObject>("CheckBtn").GetComponent<Button>();
            self.backBtn = rc.Get<GameObject>("BackBtn").GetComponent<Button>();
            self.similarity = rc.Get<GameObject>("Similarity").GetComponent<TextMeshProUGUI>();
            self.LittlesizeButton = rc.Get<GameObject>("LittleSizeBtn").GetComponent<Button>();
            self.MiddlesizeButton = rc.Get<GameObject>("MiddleSizeBtn").GetComponent<Button>();
            self.BigsizeButton = rc.Get<GameObject>("BigSizeBtn").GetComponent<Button>();
            self.clearBtn = rc.Get<GameObject>("ClearBtn").GetComponent<Button>();
            self.needItem = rc.Get<GameObject>("NeedItem");
            self.imagelist = (SpriteAtlas)ResourcesComponent.Instance.GetAsset("uisprite.unity3d", "Pixel");
            self.uibag = null;

            self.drawComponent = self.AddComponent<DrawComponent>();
            self.phashcomponent = self.AddComponent<PHashComponent>();
            self.brushColor = Color.black;
            self.MyCamera = Camera.current;
            self.pensize = 200;
            self.texsize = 1 << 6;
            self.chosenid = -1;
            self.res = 0;

            self.drawComponent.Init(self.canvasRect.GetComponent<Canvas>(), self.MyCamera, self.mydraw, self.pensize, self.brushColor).Coroutine();


            self.BindListener();
        }
    }

    [ObjectSystem]
    public class UIDrawComponentUpdateSystem : UpdateSystem<UIDrawComponent>
    {
        public override void Update(UIDrawComponent self)
        {
            //划线
            if (Input.GetMouseButtonDown(0))
            {
                self.DrawStart();
            }
            if (Input.GetMouseButton(0))
            {
                self.DrawLine();
            }
            if (Input.GetMouseButtonUp(0))
            {
                self.DrawEnd();
            }
        }
    }

    [FriendClass(typeof(UIDrawComponent))]
    public static class UIDrawComponentSystem
    {

        public static void BindListener(this UIDrawComponent self)
        {
            self.checkBtn.onClick.AddListener(self.CheckPHash);
            self.LittlesizeButton.onClick.AddListener(self.OnLittleSizeBtn);
            self.MiddlesizeButton.onClick.AddListener(self.OnMiddleSizeBtn);
            self.BigsizeButton.onClick.AddListener(self.OnBigSizeBtn);
            self.clearBtn.onClick.AddListener(self.OnClearBtn);
            self.backBtn.onClick.AddListener(self.OnBackBtn);
        }
        public static async ETTask Prepare(this UIDrawComponent self)
        {
            self.uibag = self.DomainScene().GetComponent<UIComponent>().Get(UIType.UIBag);
            if (self.uibag == null)
            {
                self.uibag = await UIHelper.Show(self.DomainScene(), UIType.UIBag, UILayer.Mid);
                UIHelper.Close(self.DomainScene(), UIType.UIBag).Coroutine();
            }
        }

        public static void ChangeColor(this UIDrawComponent self, int colorIndex)
        {
            if (colorIndex >= 0 && colorIndex < self.myColor.Length)
                self.drawComponent.SetProperty(self.myColor[colorIndex]);
            else
                Debug.LogError("input color Error");
        }

        public static void ChangeSize(this UIDrawComponent self, int s)
        {
            self.drawComponent.SetProperty(s);
        }

        public static void Clear(this UIDrawComponent self)
        {
            self.drawComponent.Clear();
            self.similarity.text = String.Empty;
        }

        public static void Save(this UIDrawComponent self)
        {
            self.drawComponent.Save();
        }

        public static void DrawStart(this UIDrawComponent self)
        {
            if (self.MyCamera == null) return;
            //Debug.Log("落笔");
            self.drawComponent.StartWrite(Input.mousePosition);
        }

        public static void DrawLine(this UIDrawComponent self)
        {
            if (self.MyCamera == null) return;

            self.drawComponent.Writing(Input.mousePosition);
        }

        public static void DrawEnd(this UIDrawComponent self)
        {
            //Debug.Log("抬笔");
        }

        public static async ETTask GetFulu(this UIDrawComponent self, int chosenId)
        {
            string name = FuluConfigCategory.Instance.GetAll()[chosenId].Name;
            self.chosenid = chosenId;
            var list = (SpriteAtlas)ResourcesComponent.Instance.GetAsset("texture.unity3d", "UIDraw");
            self.fulu.sprite = list.GetSprite(name);
            var spr = self.fulu.sprite;
            var targetTex = spr.texture; ;
            //var targetTex = new Texture2D((int)spr.rect.width, (int)spr.rect.height);
            //var pixels = self.fulu.sprite.texture.GetPixels(
            //    (int)spr.textureRect.x,
            //    (int)spr.textureRect.y,
            //    (int)spr.textureRect.width,
            //    (int)spr.textureRect.height);
            //targetTex.SetPixels(pixels);
            //targetTex.Apply();
            self.fuluTex = targetTex;
            self.SetNeedItem().Coroutine();
            await ETTask.CompletedTask;
        }

        #region
        public static void OnClearBtn(this UIDrawComponent self)
        {
            self.Clear();
        }
        public static void OnBackBtn(this UIDrawComponent self)
        {
            UIHelper.Close(self.DomainScene(), UIType.UIDraw).Coroutine();
            UIHelper.Show(self.DomainScene(), UIType.UISkillpanel, UILayer.Mid).Coroutine();
        }
        public static void OnLittleSizeBtn(this UIDrawComponent self)
        {
            self.ChangeSize(100);
        }
        public static void OnMiddleSizeBtn(this UIDrawComponent self)
        {
            self.ChangeSize(200);
        }
        public static void OnBigSizeBtn(this UIDrawComponent self)
        {
            self.ChangeSize(300);
        }

        public static async void CheckPHash(this UIDrawComponent self)
        {
            //钱不够
            if (!await self.CheckEnough())
            {
                await UIHelper.Create(self.DomainScene(), UIType.UITips, UILayer.High);
                var uitips = self.DomainScene().GetComponent<UIComponent>().Get(UIType.UITips);
                uitips.GetComponent<UITipsComponent>().SetContent(2, "You don't have enough money");
                return;
            }


            self.similarity.text = "wait a while";
            //计算
            await self.CheckPHashAsync();
            //通知结果
            await self.Notify();
            
        }

        public static async ETTask CheckPHashAsync(this UIDrawComponent self)
        {
            //压缩图片
            var t1 = self.phashcomponent.ReduceSize(self.fuluTex, self.texsize);
            var t2 = self.phashcomponent.ReduceSize(self.mydraw.texture, self.texsize);

            //转为灰度图
            t1 = self.phashcomponent.Tex2Gray(t1);
            await TimerComponent.Instance.WaitFrameAsync();
            t2 = self.phashcomponent.Tex2Gray(t2);
            await TimerComponent.Instance.WaitFrameAsync();

            //图片转矩阵
            var image1f = self.phashcomponent.image2F(t1);
            await TimerComponent.Instance.WaitFrameAsync();
            var image2f = self.phashcomponent.image2F(t2);

            //开启Task.Run
            var res = await Task.Run<float>(() =>
            {
                //DCT变换
                float[,] A = self.phashcomponent.creatDCTMatrix(self.texsize);
                float[,] AT = self.phashcomponent.Transpose(A);
                float[,] DCTA = self.phashcomponent.Multiply(self.phashcomponent.Multiply(A, image1f), AT);
                //计算均值
                float a1 = self.phashcomponent.averageDCT(DCTA);
                string phashnum_1 = self.phashcomponent.getHash(DCTA, a1);

                //DCT变换
                float[,] B = self.phashcomponent.creatDCTMatrix(self.texsize);
                float[,] BT = self.phashcomponent.Transpose(B);
                float[,] DCTB = self.phashcomponent.Multiply(self.phashcomponent.Multiply(B, image2f), BT);
                //计算均值
                float b1 = self.phashcomponent.averageDCT(DCTB);
                string phashnum_2 = self.phashcomponent.getHash(DCTB, b1);
                //计算汉明距离
                return self.phashcomponent.computeDistance(phashnum_1, phashnum_2);
            });

            self.similarity.text = (res * 100).ToString();
            self.res = (int)(res * 10);
        }

        public static async ETTask Notify(this UIDrawComponent self)
        {
            //扣除消耗
            self.uibag.GetComponent<UIBagComponent>().CostAndGenerate(self.chosenid);
            //刷新needitem列表
            self.SetNeedItem().Coroutine();
            //MainRoleComponent.Instance.ChangeNum(self.chosenid, self.res);
            await ETTask.CompletedTask;
        }
        public static async ETTask SetNeedItem(this UIDrawComponent self)
        {
            await self.Prepare();
            await self.ClearNeedItem();

            var costlist = FuluConfigCategory.Instance.Get(self.chosenid).Need;
            int end = costlist.Length;
            for (int i = 0; i < end; i += 2)
            {
                var item = RecyclePoolComponent.Instance.Get(UIType.UIDraw.StringToAB(), UIType.UIDraw, "NeedItem");
                item.GetComponent<Image>().sprite = self.imagelist.GetSprite(ItemConfigCategory.Instance.Get(costlist[i]).Name);
                item.GetComponent<ReferenceCollector>().Get<GameObject>("Text").GetComponent<TextMeshProUGUI>().text =
                    $"Need :{costlist[i + 1]}  Have:{self.uibag.GetComponent<UIBagComponent>().GetNum(costlist[i])}";
                item.transform.SetParent(self.contentRect, false);
            }
        }

        public static async ETTask ClearNeedItem(this UIDrawComponent self)
        {
            await ETTask.CompletedTask;

            while (self.contentRect.childCount > 0)
            {
                RecyclePoolComponent.Instance.Recycle(self.contentRect.GetChild(0).gameObject);
            }
        }
        public static async ETTask<bool> CheckEnough(this UIDrawComponent self)
        {
            await ETTask.CompletedTask;

            //扣除消耗
            //MainRoleComponent.Instance.ChangeNum(self.chosenid, self.res);
            return self.uibag.GetComponent<UIBagComponent>().CheckEnough(self.chosenid);

        }
        #endregion
    }
}
