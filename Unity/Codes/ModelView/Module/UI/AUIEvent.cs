namespace ET
{
    public abstract class AUIEvent
    {
        public abstract ETTask<UI> OnCreate(UIComponent uiComponent, UILayer uiLayer);
        public abstract void OnRemove(UIComponent uiComponent);

        public abstract ETTask<UI> OnShow(UIComponent uiComponent, UILayer uiLayer);

        public abstract void OnClose(UIComponent uiComponent);
    }
}