using UIKit;
using CoreGraphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Freelancehunt_Messenger.Styles.based.Entry), typeof(Freelancehunt_Messenger.iOS.Renderer.EntryBase))]
namespace Freelancehunt_Messenger.iOS.Renderer
{
    public class EntryBase : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var entry = e.NewElement as Freelancehunt_Messenger.Styles.based.Entry;
                if (entry == null)
                    return;

                // Ставим Padding от левого края
                Control.LeftView = new UIView(new CGRect(0, 0, entry.Padding.Left, 0));
                Control.LeftViewMode = UITextFieldViewMode.Always;

                // Ставим Padding от правого края
                Control.RightView = new UIView(new CGRect(0, 0, entry.Padding.Right, 0));
                Control.RightViewMode = UITextFieldViewMode.Always;

                // Цвет бордера - не работает 
                //Control.Layer.BorderColor = Color.FromHex("#0F9D58").ToCGColor();
            }
        }
    }
}
