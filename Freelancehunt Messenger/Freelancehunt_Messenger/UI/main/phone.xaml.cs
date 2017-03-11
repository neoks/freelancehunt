using Freelancehunt_Messenger.sdk;
using Freelancehunt_Messenger.sdk.Models.Threads;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Freelancehunt_Messenger.UI.main
{
    public partial class phone : ContentPage
    {
        short page = 1;
        bool IsLoadAllMessage = false;
        ObservableCollection<MessageMain> MessageMainDB = new ObservableCollection<MessageMain>();

        enum FilterMSG
        {
            Refrash,
            add
        }

        public phone()
        {
            InitializeComponent();
            navigationDrawerList.ItemsSource = MessageMainDB;
            
            GetNewMessage(FilterMSG.add);
        }


        async void GetNewMessage(FilterMSG filter)
        {
            var sdk = new SDK.Threads();

            Refresh: try
            {
                // Обновляем список ListView
                if (filter == FilterMSG.Refrash)
                {
                    IsLoadAllMessage = false;
                    MessageMainDB.Clear();
                }

                // Если уже все выведено
                if (IsLoadAllMessage == true)
                    return;

                // Получаем новые диалоги
                var Messages = await sdk.Show(page, Math.Max(PlatformInvoke.Settings.CountMsgToPage, (short)25));
                if (Messages.Count == 0) {
                    IsLoadAllMessage = true;
                    return;
                }

                // Выводим диалоги
                foreach (var item in Messages)
                {
                    item.BackgroundColor = ((MessageMainDB.Count % 2) == 0) ? Color.FromHex("#f7f8f9") : Color.FromHex("#ecedf1");
                    MessageMainDB.Add(item);
                }

                page++;
            }
            catch (Exception ex)
            {
                var res = await DisplayAlert("Ошибка запроса", ex.Message, "Обновить", "Отмена");
                if (res)
                {
                    goto Refresh;
                }
            }

            LoadIndicator.IsRunning = false;
            LoadIndicator.IsVisible = false;
        }


        public void ToolbarItem_RefrashClicked(object sender, EventArgs e)
        {
            page = 1;
            IsLoadAllMessage = true;
            LoadIndicator.IsRunning = true;
            LoadIndicator.IsVisible = true;
            GetNewMessage(FilterMSG.Refrash);
        }


        /// <summary>
        /// Переход в сообщение
        /// </summary>
        async void navigationDrawerList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            MessageMain item = navigationDrawerList.SelectedItem as MessageMain;
            if (item == null) {
                navigationDrawerList.SelectedItem = null;
                return;
            }

            navigationDrawerList.SelectedItem = null;
            await MasterPage.navPage.PushAsync(new UI.Message.Main(item.thread_id, item.from.FullName));
        }


        /// <summary>
        /// Авто подзагрузка диалогов
        /// </summary>
        private void navigationDrawerList_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            // Если пользователь указал свою цифру, то автоматичиская подзагрузка не происходит
            if (PlatformInvoke.Settings.CountMsgToPage > 25 || MessageMainDB.Count == 0)
                return;

            if (e.Item == MessageMainDB[MessageMainDB.Count - 1])
                GetNewMessage(FilterMSG.add);
        }
    }
}
