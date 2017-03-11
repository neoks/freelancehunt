using System;
using Windows.ApplicationModel.Core;
using Xamarin.Forms;

[assembly: Dependency(typeof(Freelancehunt_Messenger.Windows.engine.CloseApplication))]
namespace Freelancehunt_Messenger.Windows.engine
{
    public class CloseApplication : Models.IBased.ICloseApplication
    {
        public void Exit()
        {
            CoreApplication.Exit();
        }
    }
}
