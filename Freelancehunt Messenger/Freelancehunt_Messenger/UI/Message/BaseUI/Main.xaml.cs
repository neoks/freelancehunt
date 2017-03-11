using Freelancehunt_Messenger.Models.IBased;
using Freelancehunt_Messenger.sdk;
using Freelancehunt_Messenger.sdk.Models.Threads;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Freelancehunt_Messenger.UI.Message.BaseUI
{
    public partial class Main : ContentView
    {
        /// <summary>
        /// Сбиваем выделение item
        /// </summary>
        void navigationDrawerList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            navigationDrawerList.SelectedItem = null;
        }
        

        /// <summary>
        /// Копировать текст в буфер обмена
        /// </summary>
        private void MenuItem_ClipboardClicked(object sender, EventArgs e)
        {
            MenuItem item = sender as MenuItem;
            string text = item.BindingContext as string;
            if (text == null)
                return;

            text = Regex.Replace(text, "(<br />|<[^<]+>|<img [^>]+>|<[^>]+>|\\&lt;|\\&gt;)", rg =>
            {
                // Форматирование
                switch (rg.Groups[0].Value)
                {
                    case "&lt;": return "<";
                    case "&gt;": return ">";

                    case "</p>":
                    case "</li>":
                    case "</code>":
                    case "<hr>":
                    case "<br>":
                    case "<br />": return "\n";
                }

                // Текста в <h*>*</h*>
                if (Regex.IsMatch(rg.Groups[0].Value, "</h[0-9]+>"))
                    return "\n";

                // Изображение
                if (rg.Groups[0].Value.Contains("src="))
                {
                    return new Regex("src=\"([^\"]+)\"").Match(rg.Groups[0].Value).Groups[1].Value;
                }

                // Удаляем
                return "";
            });


            IClipboardService Clipboard = DependencyService.Get<IClipboardService>();
            Clipboard.CopyToClipboard(text);
        }


        /// <summary>
        /// Отправка сообщения
        /// </summary>
        async void OnSendTapped(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EditorMsg.Text))
                return;
            
            string SendText = EditorMsg.Text;
            EditorMsg.Text = null;

            // Временное сообщение
            MessageThread msg = new MessageThread()
            {
                post_time = DateTime.Now,
                message_html = "Отправляем...",
                from = new From() { fname = "Bot Messenger", sname = "", login = "", profile_id = PlatformInvoke.profile_id }
            };

            // Показываемм сообщение
            MessageDB.Add(msg);
            navigationDrawerList.ScrollTo(MessageDB[MessageDB.Count - 1], ScrollToPosition.MakeVisible, false);

            // Находим индекс сообщения
            int indexMSG = MessageDB.IndexOf(msg);

            // Запрос в API
            var sdk = new SDK.Threads();
            var msgResponse = await sdk.Send(ThreadID, SendText);

            // Обновляем сообщение
            if (msgResponse != null)
            {
                MessageDB[indexMSG] = msgResponse;
            }
            else
            {
                MessageDB[indexMSG] = new MessageThread()
                {
                    post_time = DateTime.Now,
                    message_html = "Не удалось отправить сообщение :(<br />" + SendText,
                    from = new From() { fname = "Bot Messenger => Ошибка запроса", sname = "", login = "BotError", profile_id = PlatformInvoke.profile_id }
                };
            }
        }


        /// <summary>
        /// Перейти в профиль пользователя
        /// </summary>
        private void GetUserProfileTapped(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            MessageThread msg = lb.BindingContext as MessageThread;
            Device.OpenUri(new Uri($"https://freelancehunt.com/profile/show/{msg.from.login}.html"));
        }


        /// <summary>
        /// Увеличиваем input в зависимости от количиства "\n\r"
        /// </summary>
        static double DefaultWidthTextToHeight;
        private void EditorMsg_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DefaultWidthTextToHeight == 0 && WidthText.Height != -1)
                DefaultWidthTextToHeight = WidthText.Height;

            // Высота по умолчанию
            double DefaultHeight = 40; //52

            // Выводим "Please Holder"
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                PleaseHolderEditor.IsVisible = true;
                StackLayoutSendMsg.HeightRequest = DefaultHeight;
                return;
            }

            // Прячем "Please Holder"
            PleaseHolderEditor.IsVisible = false;

            // Выводим текст что-бы получить размер
            WidthText.FormattedText = new FormattedString();
            WidthText.FormattedText.Spans.Add(new Span() { Text = e.NewTextValue, FontSize = 16 });


            // Размер input
            double HeightRequest = WidthText.Height;

            // Максимальное количиство новых строк
            byte MaxNewLine = Device.Idiom == TargetIdiom.Phone ? (byte)4 : (byte)7;


            // Добавить строку если в конце \n или \r
            if (e.OldTextValue != null && e.NewTextValue.Length > e.OldTextValue.Length && Regex.IsMatch(e.NewTextValue, "([\n\r])$"))
                HeightRequest += DefaultWidthTextToHeight;

            // Отнять строку если в конце \n или \r
            if (e.OldTextValue != null && e.NewTextValue.Length < e.OldTextValue.Length && Regex.IsMatch(e.OldTextValue, "([\n\r])$"))
                HeightRequest -= 15; // andoid

            // Максимальное количиство строк
            HeightRequest = MaxNewLine * DefaultWidthTextToHeight > HeightRequest ? HeightRequest : MaxNewLine * DefaultWidthTextToHeight;

            // Размер input
            HeightRequest = HeightRequest > DefaultHeight ? HeightRequest + DefaultWidthTextToHeight : DefaultHeight;
            StackLayoutSendMsg.HeightRequest = HeightRequest;
        }


        /// <summary>
        /// Кнопка "Загрузить еще"
        /// Выводит сообщения из базы на экран
        /// </summary>
        private void MoreMSG_Clicked(object sender, EventArgs e)
        {
            LoadNewMessage(MessageComand.add);
        }
    }
}
