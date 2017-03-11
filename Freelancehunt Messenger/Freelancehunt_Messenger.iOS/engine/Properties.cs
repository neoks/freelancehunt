using Freelancehunt_Messenger.Models.based;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(Freelancehunt_Messenger.iOS.engine.Properties))]
namespace Freelancehunt_Messenger.iOS.engine
{
    public class Properties : Models.IBased.IProperties
    {
        public string Read(KeyProperties key)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, key.ToString());


            if (!File.Exists(path))
                return "";

            return File.ReadAllText(path);
        }

        public void Write(KeyProperties key, string arg)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, key.ToString());

            File.WriteAllText(path, arg);
        }
    }
}
