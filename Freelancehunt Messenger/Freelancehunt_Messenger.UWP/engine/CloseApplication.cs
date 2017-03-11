using System;
using Windows.ApplicationModel.Core;
using Xamarin.Forms;

[assembly: Dependency(typeof(Freelancehunt_Messenger.UWP.engine.CloseApplication))]
namespace Freelancehunt_Messenger.UWP.engine
{
    public class CloseApplication : Models.IBased.ICloseApplication
    {
        public void Exit()
        {
            CoreApplication.Exit();
        }
    }
}
