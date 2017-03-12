using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(Freelancehunt_Messenger.Styles.based.Editor), typeof(Freelancehunt_Messenger.UWP.Renderer.EditorBase))]
namespace Freelancehunt_Messenger.UWP.Renderer
{
    public class EditorBase : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                // Размер бордера от разных краев
                Control.BorderThickness = new Thickness(0);

                // Распаковка
                editor = e.NewElement as Freelancehunt_Messenger.Styles.based.Editor;
                if (editor == null)
                    return;

                // События
                Control.KeyDown += Tb_KeyDown;
                Control.KeyUp += Tb_KeyUp;
            }
        }


        #region ctrl + enter
        Windows.System.VirtualKey LastKeyDown;
        Freelancehunt_Messenger.Styles.based.Editor editor;

        private void Tb_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (editor == null)
                return;

            if (e.Key == Windows.System.VirtualKey.Enter && LastKeyDown == Windows.System.VirtualKey.Control)
            {
                editor.SetEvent_CtrlEnter();
            }

            LastKeyDown = Windows.System.VirtualKey.Delete;
        }

        private void Tb_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (editor == null)
                return;

            LastKeyDown = e.Key;
        }
        #endregion
    }
}
