using System;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Freelancehunt_Messenger.iOS.engine.ClipboardService))]
namespace Freelancehunt_Messenger.iOS.engine
{
    public class ClipboardService : Models.IBased.IClipboardService
    {
        public void CopyToClipboard(string text)
        {
            UIPasteboard clipboard = UIPasteboard.General;
            clipboard.String = text;
        }
    }
}
