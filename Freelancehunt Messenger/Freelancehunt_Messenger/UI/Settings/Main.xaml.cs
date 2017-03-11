using Freelancehunt_Messenger.Models.based;
using Freelancehunt_Messenger.Models.IBased;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace Freelancehunt_Messenger.UI.Settings
{
    public partial class Main : ContentPage
    {
        IProperties Properties;

        public Main()
        {
            InitializeComponent();
            Properties = DependencyService.Get<IProperties>();

            var cat = NewCategory("", 0);
            cat.Add(CountMsgToPage("Диалогов", "Количиство диалогов на одну страницу"));
            cat.Add(CountMsgToDialog("Сообщений", "Количество подгружаемых сообщений в диалогах"));
        }


        /// <summary>
        /// Сохраняет настройки на диске
        /// </summary>
        private void SaveSetings()
        {
            Properties.Write(KeyProperties.Settings, JsonConvert.SerializeObject(PlatformInvoke.Settings));
        }


        /// <summary>
        /// Новая категория
        /// </summary>
        /// <param name="Title">Имя категории</param>
        /// <param name="MarginTop">Отступ сверху</param>
        /// <returns>StackLayout.Children</returns>
        private IList<View> NewCategory(string Title, double MarginTop = 20)
        {
            // Контрол компановки
            StackLayout stkl = new StackLayout();

            // Отдаем сам контролы что-бы сюда добовлять другие контролы
            StackLayoutMain.Children.Add(stkl);
            return stkl.Children;
        }


        /// <summary>
        /// Диалогов
        /// </summary>
        /// <param name="Title">Имя контрола</param>
        /// <param name="Description">Описание контрола</param>
        private View CountMsgToPage(string Title, string Description)
        {
            return new BaseControl(Title, Description, (s, e) => 
            {
                PlatformInvoke.Settings.CountMsgToPage = (short)e.NewValue;
                SaveSetings();

            }, PlatformInvoke.Settings.CountMsgToPage, "авто", 5, 60, 25);
        }


        /// <summary>
        /// Сообщений
        /// </summary>
        /// <param name="Title">Имя контрола</param>
        /// <param name="Description">Описание контрола</param>
        private View CountMsgToDialog(string Title, string Description)
        {
            return new BaseControl(Title, Description, (s, e) =>
            {
                PlatformInvoke.Settings.CountMsgToDialog = (short)e.NewValue;
                SaveSetings();

            }, PlatformInvoke.Settings.CountMsgToDialog, "авто", 5, 0, 10);
        }
    }
}
