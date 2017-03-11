using System;
using Xamarin.Forms;

namespace Freelancehunt_Messenger.Styles.based
{
    public class Button : Xamarin.Forms.Button
    {
        /// <summary>
        /// Отступы текста от краев
        /// </summary>
        public Thickness Padding { get; protected set; }

        /// <summary>
        /// Размер бордеров
        /// </summary>
        public Thickness BorderThickness { get; protected set; }


        public Button()
        {
            BorderColor = (Color)App.Current.Resources["BorderColor_Button"];         // Цвет бордера
            TextColor = (Color)App.Current.Resources["TextColor_1"];                  // Цвет текста


            FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label));         // Размер текста 
            HeightRequest = 60;
            MinimumHeightRequest = HeightRequest;


            // Цвет заднего фона кнопки
            BackgroundColor = (Color)App.Current.Resources["BackgroundColor_Button"];
        }
    }
}
