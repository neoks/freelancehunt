using System;
using Xamarin.Forms;
using Freelancehunt_Messenger.Models.IBased;
using Freelancehunt_Messenger.Models.based;
using Freelancehunt_Messenger.sdk;
using System.Diagnostics;

namespace Freelancehunt_Messenger.UI.auth.BaseUI
{
    public partial class MainView : ContentView
    {
        ContentPage main;
        IProperties Properties;

        public MainView(ContentPage _main)
        {
            InitializeComponent();
            this.main = _main;

            Properties = DependencyService.Get<IProperties>();
            EntryToken.Text = Properties.Read(KeyProperties.apiToken);
            EntrySecret.Text = Properties.Read(KeyProperties.apiSecret);
        }

        async void Button_AuthClicked(object sender, EventArgs e)
        {
            ButtonAuth.IsEnabled = false;
            Properties.Write(KeyProperties.apiToken, EntryToken.Text);
            Properties.Write(KeyProperties.apiSecret, EntrySecret.Text);

            // Данные для авторизации в API
            DefaultRestClient.apiToken = EntryToken.Text;
            DefaultRestClient.apiSecret = EntrySecret.Text;


            try
            {
                // Данные профиля
                var Profiles = new SDK.Profiles();
                sdk.Models.Profiles.ME me = await Profiles.me;
                PlatformInvoke.profile_id = me.profile_id;
                
                // Если все ок
                Application.Current.MainPage = new UI.main.MasterPage(me);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("401 "))
                {
                    await this.main.DisplayAlert("Ошибка запроса", "Неправильный \"Идентификатор\" или \"Секретный ключ\", проверьте правильность вводимых данных. Если ошибка сохраняется, напишите в \"Техподдержку freelancehunt.com\", с просьбой проверить доступ к API вашего аккаунта.", "OK");
                }
                else
                {
                    await this.main.DisplayAlert("Ошибка запроса", ex.Message, "OK");
                }
            }

            ButtonAuth.IsEnabled = true;
        }


        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            // Открыть URL
            Device.OpenUri(new Uri("https://freelancehunt.com/service/privacy"));
        }
    }
}
