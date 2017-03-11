using Android.Graphics.Drawables;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Freelancehunt_Messenger.Styles.based.Editor), typeof(Freelancehunt_Messenger.Droid.Renderer.EditorBase))]
namespace Freelancehunt_Messenger.Droid.Renderer
{
    public class EditorBase : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                var entry = e.NewElement as Freelancehunt_Messenger.Styles.based.Editor;
                if (entry == null)
                    return;

                GradientDrawable customBG = new GradientDrawable();
                customBG.SetColor(Android.Graphics.Color.Transparent);
                customBG.SetCornerRadius(0);
                int borderWidth = 0;
                customBG.SetStroke(borderWidth, Android.Graphics.Color.Black);
                this.SetBackground(customBG);

                Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
            }
        }
    }
}