using System;
using Xamarin.Forms;

namespace Freelancehunt_Messenger.UI.auth
{
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();

            var baseUI = new BaseUI.MainView(this);


            if (Device.Idiom == TargetIdiom.Desktop || Device.OS == TargetPlatform.Android)
            {
                this.Content = baseUI;
            }
            else // IOS, Windows Phone
            {
                this.Content = new ScrollView()
                {
                    Content = baseUI,
                    Margin = new Thickness(0, 10, 0, 0)
                };
            }
        }
    }
}
