using Freelancehunt_Messenger.Models.IBased;
using Freelancehunt_Messenger.Models.main;
using Freelancehunt_Messenger.sdk;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Freelancehunt_Messenger.UI.main
{
    public partial class MasterPage : MasterDetailPage
    {
        phone ph = new phone();
        static MasterPage main;

        public static NavigationPage navPage => main.NavPage;

        public MasterPage(sdk.Models.Profiles.ME me)
        {
            InitializeComponent();
            main = this;

            //if (Device.OS == TargetPlatform.Windows && Device.Idiom == TargetIdiom.Desktop)
            //    Lb_Copyrate.IsVisible = false;

            // Заполняем меню кнопками
            AddMenuItem("Лента", "flash.png", OnTappedFeed);
            AddMenuItem("Сообщения", "message.png", OnTappedMessage);
            //AddMenuItem("Подписки", "settings.png", OnTappedHome);
            AddMenuItem("Настройки", "settings.png", OnTappedSettings);
            AddMenuItem("Выход", "exit.png", OnTappedExit);


            // Присваем список в "navigationDrawerList"
            navigationDrawerList.ItemsSource = menuList;


            // Данные профайла
            lb_Login.Text = me.login;
            lb_UserName.Text = me.fname + " " + me.sname;
            img_Avatar.Source = me.avatar;

            
            NavPage.PushAsync(ph);
        }


        async void OnTappedMessage()
        {
            await NavPage.PopToRootAsync();
            ph.ToolbarItem_RefrashClicked(null ,null);
        }

        async private void OnTappedSettings()
        {
            await NavPage.PushAsync(new UI.Settings.Main());
        }

        async private void OnTappedFeed()
        {
            await NavPage.PushAsync(new UI.Feed.Main());
        }


        private void OnTappedExit()
        {
            // Завершаем программу
            ICloseApplication app = DependencyService.Get<ICloseApplication>();
            app.Exit();
        }
        

        /// <summary>
        /// Список кнопок в меню
        /// </summary>
        private List<Models.main.MenuItem> menuList = new List<Models.main.MenuItem>();



        /// <summary>
        /// Добовление кнопки в меню
        /// </summary>
        /// <param name="title">Имя кнопки</param>
        /// <param name="icon">Иконка</param>
        /// <param name="onTapped">Метод который нужно вызывать при клике на кнопку</param>
        private void AddMenuItem(string title, string icon, EventMenuItem onTapped)
        {
            if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
                icon = "img/icon/" + icon;

            menuList.Add(new Models.main.MenuItem() { Title = title, Icon = icon, OnTapped = onTapped });
        }


        /// <summary>
        /// Вызывается при клике на любой item в меню
        /// </summary>
        private void navigationDrawerList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // Логика для планшета
            if (Device.Idiom == TargetIdiom.Tablet && this.Width != -1 && this.Height != -1 && this.Height > this.Width)
                IsPresented = false; // Закрываем меню

            // Логика для телефона
            if (Device.Idiom == TargetIdiom.Phone)
                IsPresented = false; // Закрываем меню


            // Распаковка выбраного элемента
            var item = e.Item as Models.main.MenuItem;
            if (item == null)
                return;

            // Вызов делегата для выбраного элемента
            item.OnTapped.Invoke();

            // Обновление прошлого элемента
            ((ListView)sender).SelectedItem = null;
        }
    }
}
