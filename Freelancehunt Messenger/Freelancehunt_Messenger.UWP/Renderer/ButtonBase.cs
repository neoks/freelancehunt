using System;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(Freelancehunt_Messenger.Styles.based.Button), typeof(Freelancehunt.UWP.Renderer.ButtonBase))]
namespace Freelancehunt.UWP.Renderer
{
    public class ButtonBase : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var button = e.NewElement as Freelancehunt_Messenger.Styles.based.Button;
                if (button == null)
                    return;
                
                // Отступ текста от краев кнопки
                Control.Padding = new Thickness(button.Padding.Left, button.Padding.Top, button.Padding.Right, button.Padding.Bottom);

                // Размер бордера от разных краев
                Control.BorderThickness = new Thickness(button.BorderThickness.Left, button.BorderThickness.Top, button.BorderThickness.Right, button.BorderThickness.Bottom);
            }
        }
    }
}
