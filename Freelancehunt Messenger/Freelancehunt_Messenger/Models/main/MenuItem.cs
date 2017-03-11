using System;
using Xamarin.Forms;
using Freelancehunt_Messenger.Models.based;

namespace Freelancehunt_Messenger.Models.main
{
    /// <summary>
    /// Делегат для вызова событий
    /// </summary>
    /// <param name="item">MasterPageMenuItem</param>
    public delegate void EventMenuItem();

    public class MenuItem : SelectedItem
    {
        private string icon;
        bool IsWindows = Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone;


        /// <summary>
        /// Имя кнопки
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// Иконка
        /// </summary>
        public string Icon
        {
            get
            {
                //return (IsWindows ? "img/icon/" : "icon_") + icon;
                return icon;
            }
            set { icon = value; }
        }

        /// <summary>
        /// Событие которое будет вызвано при выборе данной кнопки в меню
        /// </summary>
        public EventMenuItem OnTapped { get; set; }
    }
}
