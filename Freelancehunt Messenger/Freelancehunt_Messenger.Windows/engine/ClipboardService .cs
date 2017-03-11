using System;
using Windows.ApplicationModel.DataTransfer;
using Xamarin.Forms;

[assembly: Dependency(typeof(Freelancehunt_Messenger.Windows.engine.ClipboardService))]
namespace Freelancehunt_Messenger.Windows.engine
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
