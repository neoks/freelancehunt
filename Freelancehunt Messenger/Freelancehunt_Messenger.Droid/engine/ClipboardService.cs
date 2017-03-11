using Android.Content;
using Xamarin.Forms;

[assembly: Dependency(typeof(Freelancehunt_Messenger.Droid.engine.ClipboardService))]
namespace Freelancehunt_Messenger.Droid.engine
{
    public class ClipboardService : Models.IBased.IClipboardService
    {
        public void CopyToClipboard(string text)
        {
            // Get the Clipboard Manager
            var clipboardManager = (ClipboardManager)Forms.Context.GetSystemService(Context.ClipboardService);

            // Create a new Clip
            ClipData clip = ClipData.NewPlainText("", text);

            // Copy the text
            clipboardManager.PrimaryClip = clip;
        }
    }
}