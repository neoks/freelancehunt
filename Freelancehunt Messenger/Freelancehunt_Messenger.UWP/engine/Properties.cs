using Freelancehunt_Messenger.Models.based;
using System;
using System.IO;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(Freelancehunt_Messenger.UWP.engine.Properties))]
namespace Freelancehunt_Messenger.UWP.engine
{
    public class Properties : Models.IBased.IProperties
    {
        public string Read(KeyProperties key)
        {
            if (!File.Exists(Path.Combine(ApplicationData.Current.LocalFolder.Path, key.ToString())))
                return "";

            return File.ReadAllText(Path.Combine(ApplicationData.Current.LocalFolder.Path, key.ToString()));
        }

        public void Write(KeyProperties key, string arg)
        {
            File.WriteAllText(Path.Combine(ApplicationData.Current.LocalFolder.Path, key.ToString()), arg);
        }
    }
}
