using Freelancehunt_Messenger.Models.based;
using Freelancehunt_Messenger.Models.IBased;
using Newtonsoft.Json;
using System;
using Xamarin.Forms;

namespace Freelancehunt_Messenger
{
    public static class PlatformInvoke
    {
        static PlatformInvoke()
        {
            IProperties Properties = DependencyService.Get<IProperties>();
            Settings = JsonConvert.DeserializeObject<SettingsModel>(Properties.Read(KeyProperties.Settings));

            if (Settings == null) {
                Settings = new SettingsModel() { CountMsgToPage = 25, CountMsgToDialog = 0 };
            }
        }

        public static SettingsModel Settings { get; private set; }

        public static int profile_id { get; set; }

        public static bool IsWindows => Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone;
    }
}
