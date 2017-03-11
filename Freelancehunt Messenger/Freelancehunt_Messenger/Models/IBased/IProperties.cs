using Freelancehunt_Messenger.Models.based;

namespace Freelancehunt_Messenger.Models.IBased
{
    public interface IProperties
    {
        string Read(KeyProperties key);

        void Write(KeyProperties key, string arg);
    }
}
