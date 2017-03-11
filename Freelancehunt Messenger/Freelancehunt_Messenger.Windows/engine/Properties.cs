using Freelancehunt_Messenger.Models.based;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(Freelancehunt_Messenger.Windows.engine.Properties))]
namespace Freelancehunt_Messenger.Windows.engine
{
    public class Properties : Models.IBased.IProperties
    {
        public string Read(KeyProperties key)
        {
            return "";
        }


        public void Write(KeyProperties key, string arg)
        {
        }
    }
}
