using Freelancehunt_Messenger.sdk;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace Freelancehunt_Messenger.UI.Feed
{
    public partial class Main : ContentPage
    {
        /// <summary>
        /// Список данных
        /// </summary>
        List<sdk.Models.MY.feed> FeedDB;


        public Main()
        {
            InitializeComponent();
            FeedDB = new List<sdk.Models.MY.feed>();
            FeedDB.Capacity = 30;
            Renderer();
        }


        /// <summary>
        /// Обновить страницу
        /// </summary>
        private void ToolbarItem_RefrashClicked(object sender, EventArgs e)
        {
            navigationDrawerList.ItemsSource = null;
            FeedDB = new List<sdk.Models.MY.feed>();
            FeedDB.Capacity = 30;
            Renderer();
        }


        /// <summary>
        /// Сбиваем выделение item
        /// </summary>
        void navigationDrawerList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            navigationDrawerList.SelectedItem = null;
        }


        /// <summary>
        /// Перейти в профиль пользователя
        /// </summary>
        private void GetUserProfileTapped(object sender, EventArgs e)
        {
            StackLayout st = sender as StackLayout;
            sdk.Models.MY.feed fd = st.BindingContext as sdk.Models.MY.feed;
            Device.OpenUri(new Uri($"https://freelancehunt.com/profile/show/{fd.from.login}.html"));
        }


        async void Renderer()
        {
            LoadIndicator.IsRunning = true;
            LoadIndicator.IsVisible = true;

            Refresh: try
            {
                var sdk = new SDK.my();
                foreach (var item in await sdk.Feed)
                {
                    if (item.message.Length > 1)
                        item.message =  char.ToUpper(item.message[0]) + item.message.Substring(1);

                    FeedDB.Add(item);
                }

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
            navigationDrawerList.ItemsSource = FeedDB;
        }
    }
}
