using System;
using Windows.UI.Xaml;
using Xamarin.Forms.Platform.WinRT;

[assembly: ExportRenderer(typeof(Freelancehunt_Messenger.Styles.based.Editor), typeof(Freelancehunt_Messenger.Windows.Renderer.EditorBase))]
namespace Freelancehunt_Messenger.Windows.Renderer
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
            }
        }
    }
}
