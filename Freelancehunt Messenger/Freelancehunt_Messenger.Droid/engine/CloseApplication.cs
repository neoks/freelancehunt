using Android.App;
using Xamarin.Forms;

[assembly: Dependency(typeof(Freelancehunt_Messenger.Droid.engine.CloseApplication))]
namespace Freelancehunt_Messenger.Droid.engine
{
    public class CloseApplication : Models.IBased.ICloseApplication
    {
        public void Exit()
        {
            int pid = Android.OS.Process.MyPid();

            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();

            // Окончательно добиваем
            Android.OS.Process.KillProcess(pid);
        }
    }
}