using Freelancehunt_Messenger.sdk;
using Freelancehunt_Messenger.sdk.Models.Threads;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Freelancehunt_Messenger.UI.Message
{
    public partial class Main : ContentPage
    {
        BaseUI.Main baseUI;

        public Main(int thread_id, string userName)
        {
            InitializeComponent();
            if (Device.Idiom == TargetIdiom.Phone)
            {
                this.Title = "Переписка";
            }
            else
            {
                this.Title = "Переписка с " + userName;
            }


            baseUI = new BaseUI.Main(thread_id);
            if (Device.OS == TargetPlatform.iOS)
            {
                this.Content = new ScrollView()
                {
                    Content = baseUI
                };
            }
            else
            {
                this.Content = baseUI;
            }
        }
        

        private void ToolbarItem_RefrashClicked(object sender, EventArgs e)
        {
            baseUI.RefrashClicked();
        }
    }
}
