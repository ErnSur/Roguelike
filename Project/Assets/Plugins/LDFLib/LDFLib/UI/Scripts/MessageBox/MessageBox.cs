using System;

namespace LDF.UserInterface.MessageBox
{
    public static class MessageBox
    {
        public static void Show(MessageBoxContent content, Action showCallback = null, Action hideCallback = null)
        {
            MessageBoxPresenter.Instance.ShowMessageBox(content, showCallback, hideCallback);
        }
    }
}