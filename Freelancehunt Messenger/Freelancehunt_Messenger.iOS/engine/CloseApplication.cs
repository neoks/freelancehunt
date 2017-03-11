using System;
using System.Threading;
using Xamarin.Forms;

[assembly: Dependency(typeof(Freelancehunt_Messenger.iOS.engine.CloseApplication))]
namespace Freelancehunt_Messenger.iOS.engine
{
    public class CloseApplication : Models.IBased.ICloseApplication
    {
        public void Exit()
        {
            Thread.CurrentThread.Abort();
            throw new Exception();
        }
    }
}
