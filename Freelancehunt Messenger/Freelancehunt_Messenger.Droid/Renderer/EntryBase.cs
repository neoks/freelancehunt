using Android.Graphics;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Freelancehunt_Messenger.Styles.based.Entry), typeof(Freelancehunt_Messenger.Droid.Renderer.EntryBase))]
namespace Freelancehunt_Messenger.Droid.Renderer
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
                
                // Ставим Padding
                Control.SetPadding((int)entry.Padding.Left, (int)entry.Padding.Top, (int)entry.Padding.Right, (int)entry.Padding.Bottom);

                // Цвет полоски
                Control.Background.SetColorFilter(Xamarin.Forms.Color.FromHex(entry.BorderBrush).ToAndroid(), PorterDuff.Mode.SrcAtop);
            }
        }
    }
}