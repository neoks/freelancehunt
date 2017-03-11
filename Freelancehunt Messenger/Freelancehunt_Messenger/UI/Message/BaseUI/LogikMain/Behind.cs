using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Freelancehunt_Messenger.UI.Message.BaseUI
{
    /// <summary>
    /// Behind
    /// </summary>
    public partial class Main : ContentView
    {
        public void RefrashClicked()
        {
            LoadNewMessage(MessageComand.Refrash);
        }
        
    }
}
