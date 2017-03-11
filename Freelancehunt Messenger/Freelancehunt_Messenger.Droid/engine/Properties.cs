using Freelancehunt_Messenger.Models.based;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(Freelancehunt_Messenger.Droid.engine.Properties))]
namespace Freelancehunt_Messenger.Droid.engine
{
    public class Properties : Models.IBased.IProperties
    {
        public string Read(KeyProperties key)
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, key.ToString());


            if (!File.Exists(path))
                return "";

            return File.ReadAllText(path);
        }

        public void Write(KeyProperties key, string arg)
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, key.ToString());

            File.WriteAllText(path, arg);
        }
    }
}