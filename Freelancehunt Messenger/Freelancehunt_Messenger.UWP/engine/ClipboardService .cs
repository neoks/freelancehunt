using System;
using Windows.ApplicationModel.DataTransfer;
using Xamarin.Forms;

[assembly: Dependency(typeof(Freelancehunt_Messenger.UWP.engine.ClipboardService))]
namespace Freelancehunt_Messenger.UWP.engine
{
    public class ClipboardService : Models.IBased.IClipboardService
    {
        public void CopyToClipboard(String text)
        {
            DataPackage dataPackage = new DataPackage();
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            dataPackage.SetText(text);

            Clipboard.SetContent(dataPackage);
        }

    }
}
