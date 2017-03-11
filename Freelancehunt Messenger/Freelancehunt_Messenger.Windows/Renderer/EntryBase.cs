using System;
using System.ComponentModel;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Xamarin.Forms.Platform.WinRT;

[assembly: ExportRenderer(typeof(Freelancehunt_Messenger.Styles.based.Entry), typeof(Freelancehunt_Messenger.Windows.Renderer.EntryBase))]
namespace Freelancehunt_Messenger.Windows.Renderer
{
    public class EntryBase : EntryRenderer
    {
        Freelancehunt_Messenger.Styles.based.Entry entry;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                entry = e.NewElement as Freelancehunt_Messenger.Styles.based.Entry;
                if (entry == null)
                    return;
                
                // Размер бордера от разных краев
                Control.BorderThickness = new Thickness(entry.BorderThickness.Left, entry.BorderThickness.Top, entry.BorderThickness.Right, entry.BorderThickness.Bottom);

                // Цвет бордера
                Control.BorderBrush = GetSolidColorBrush(entry.BorderBrush);

                // Любое событие в Entry
                entry.PropertyChanged += Entry_PropertyChanged;
            }
        }


        /// <summary>
        /// 0 - умолчание
        /// 1 - Entry в фокусе
        /// 2 - Entry без фокуса
        /// </summary>
        byte IsFocused = 0;
        private void Entry_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (entry != null)
            {
                if (entry.IsFocused && !string.IsNullOrEmpty(entry.Text))
                {
                    if (IsFocused == 2)
                    {
                        // Если Entry в фокусе, то смешаем текст в лево, что-бы влезла кнопка "x" и текст остался по середине
                        Control.Padding = new Thickness(entry.Padding.Left, entry.Padding.Top, entry.Padding.Right, entry.Padding.Bottom);
                        IsFocused = 1;
                    }
                }
                else
                {
                    if (IsFocused == 0 || IsFocused == 1)
                    {
                        // Если Entry без фокуса, то меняем данные Padding, что-бы текст был по середине
                        Control.Padding = new Thickness(entry.Padding.Left, entry.Padding.Top, entry.Padding.Right + entry.Padding.Left, entry.Padding.Bottom);
                        IsFocused = 2;
                    }
                }
            }
        }


        /// <summary>
        /// Костыль для конвертации цвета
        /// </summary>
        /// <param name="hex">цвет</param>
        public SolidColorBrush GetSolidColorBrush(string hex)
        {
            hex = hex.Replace("#", string.Empty);
            byte a = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte r = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(6, 2), 16));

            Color color = new Color() { A = a, B = b, G = g, R = r };
            SolidColorBrush myBrush = new SolidColorBrush(color);
            return myBrush;
        }
    }
}
