using System;
using Windows.UI.Xaml;
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
            }
        }
    }
}
