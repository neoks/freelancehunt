using System;
using Xamarin.Forms;

namespace Freelancehunt_Messenger.Styles.based
{
    public class Entry : Xamarin.Forms.Entry
    {
        public Entry()
        {
            BackgroundColor = (Color)App.Current.Resources["TransparentColor"];     // Цвет заднего фона
            TextColor = (Color)App.Current.Resources["TextColor_3"];                // Цвет текста
            PlaceholderColor = (Color)App.Current.Resources["TextColor_2"];         // Цвет текста подсказки
            BorderBrush = (string)App.Current.Resources["EntryColor_BorderBrush"];  // Цвет бордера

            HorizontalTextAlignment = TextAlignment.Center;                         // Выравнивать текст по середине
            BorderThickness = new Thickness(0, 0, 0, 1);                            // Размер бордеров
            FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));         // Размер текста 
            HeightRequest = 45;                                                     // Максимальный размер и размер по умолчанию

            switch (Device.OS)
            {
                case TargetPlatform.Windows:
                case TargetPlatform.WinPhone:
                    {
                        Padding = new Thickness(43, 4, 0, 0);
                        break;
                    }
                case TargetPlatform.Android:
                    {
                        Padding = new Thickness(70, 0, 50, 0);
                        break;
                    }
                case TargetPlatform.iOS:
                    {
                        Padding = new Thickness(35, 0, 35, 0);
                        break;
                    }
            }
        }

        /// <summary>
        /// Отступы текста от краев
        /// </summary>
        public Thickness Padding { get; internal set; }

        /// <summary>
        /// Размер бордеров
        /// </summary>
        public Thickness BorderThickness { get; private set; }

        /// <summary>
        /// Цвет бордера
        /// </summary>
        public string BorderBrush { get; internal set; }
    }
}
