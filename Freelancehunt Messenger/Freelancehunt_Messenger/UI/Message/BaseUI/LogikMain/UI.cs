using System;
using Xamarin.Forms;

namespace Freelancehunt_Messenger.UI.Message.BaseUI
{
    /// <summary>
    /// UI
    /// </summary>
    public partial class Main : ContentView
    {
        int ThreadID;

        public Main(int thread_id)
        {
            InitializeComponent();
            StackLayoutSendMsg.HeightRequest = 40;//52

            ThreadID = thread_id;
            navigationDrawerList.ItemsSource = MessageDB;
            LoadNewMessage(MessageComand.Refrash);
        }
    }
}
