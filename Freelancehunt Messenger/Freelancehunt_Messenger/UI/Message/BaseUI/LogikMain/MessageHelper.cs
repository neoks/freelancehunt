using Freelancehunt_Messenger.sdk;
using Freelancehunt_Messenger.sdk.Models.Threads;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Freelancehunt_Messenger.UI.Message.BaseUI
{
    /// <summary>
    /// MessageHelper
    /// </summary>
    public partial class Main : ContentView
    {
        /// <summary>
        /// Список сообщений - (вывод)
        /// </summary>
        ObservableCollection<MessageThread> MessageDB = new ObservableCollection<MessageThread>();

        /// <summary>
        /// Список сообщений - (откуда брать даные)
        /// </summary>
        List<MessageThread> MessageHideDB = new List<MessageThread>();

        enum MessageComand
        {
            Refrash,
            add
        }


        /// <summary>
        /// Загрузка сообщений
        /// </summary>
        /// <param name="comand">FirstLoad/Refrash/GetAllMessage</param>
        async void LoadNewMessage(MessageComand comand)
        {
            try
            {
                #region Обновление базы "MessageHideDB"
                if (comand == MessageComand.Refrash)
                {
                    // Чистим текущие сообщения
                    MessageDB.Clear();
                    LoadIndicator.IsRunning = true;
                    LoadIndicator.IsVisible = true;
                    Resources["HeightButtonToMoreMSG"] = Device.OS == TargetPlatform.Android ? 42 : 35;
                    Resources["IsViewButtonToMoreMSG"] = true;
                    Resources["TextButtonToMoreMSG"] = $"Показать еще";

                    // Получаем все сообщения c API
                    var sdk = new SDK.Threads();
                    MessageHideDB = await sdk.Show(ThreadID);
                }
                #endregion


                // Пользовательское количиство сообщений
                int CountMsgToDialog = PlatformInvoke.Settings.CountMsgToDialog <= 10 ? 40 : PlatformInvoke.Settings.CountMsgToDialog;

                // Высчитываем количиство смс для подзагрузки
                int position = (MessageHideDB.Count - MessageDB.Count) - 1;
                int MaxLoadMSG = Math.Min(CountMsgToDialog - 1, position);
                if (MaxLoadMSG >= 0)
                {
                    // Выводим сообщения
                    for (int i = position; i >= position - MaxLoadMSG; i--)
                        MessageDB.Insert(0, MessageHideDB[i]);

                    // Скролим к сообщению
                    if (comand == MessageComand.Refrash)
                    {
                        navigationDrawerList.ScrollTo(MessageDB[MessageDB.Count - 1], ScrollToPosition.MakeVisible, false);
                    }
                    else
                    {
                        navigationDrawerList.ScrollTo(MessageDB[Math.Min(MaxLoadMSG + 1, MessageDB.Count)], ScrollToPosition.End, false);
                    }

                    // Обновляем данные
                    position = MessageHideDB.Count - MessageDB.Count;
                    MaxLoadMSG = Math.Min(CountMsgToDialog, position);

                    // Меняем текст кнопки "MoreMSG"
                    Resources["TextButtonToMoreMSG"] = $"Показать еще {MaxLoadMSG} из {position}";
                }

                // Прячем кнопку загрузить еще
                if (MessageHideDB.Count == MessageDB.Count)
                {
                    Resources["IsViewButtonToMoreMSG"] = false;
                    Resources["HeightButtonToMoreMSG"] = 0;
                }

                // Прячем текст загрузки
                LoadIndicator.IsRunning = false;
                LoadIndicator.IsVisible = false;
            }
            catch (Exception ex)
            {
                LoadIndicator.IsRunning = false;
                LoadIndicator.IsVisible = false;
                MessageDB.Add(new MessageThread()
                {
                    from = new From() { fname = "Bot Messenger => Ошибка запроса", sname = "", login = "BotError", profile_id = PlatformInvoke.profile_id },
                    post_time = DateTime.Now,
                    message_html = ex.Message
                });
            }
        }
    }
}
