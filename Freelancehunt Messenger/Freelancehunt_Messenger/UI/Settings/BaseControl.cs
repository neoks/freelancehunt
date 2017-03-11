using System;
using Xamarin.Forms;

namespace Freelancehunt_Messenger.UI.Settings
{
    public class BaseControl : Xamarin.Forms.StackLayout
    {
        #region Инициализация контролов
        Grid grid = new Grid();

        Label title = new Label();
        Label description = new Label();

        BoxView boxView = new BoxView();
        #endregion


        /// <summary>
        /// Stepper
        /// </summary>
        /// <param name="Title">Имя контрола</param>
        /// <param name="Description">Описание контрола</param>
        /// <param name="valueChanged">Событие Stepper</param>
        /// <param name="value">Значения по умолчанию</param>
        public BaseControl(string Title, string Description, EventHandler<ValueChangedEventArgs> valueChanged, double value, string ZeroText, double Increment = 1, double Maximum = 0, double Minimum = 0)
        {
            StackLayout stkl = new StackLayout();

            Label lb = new Label()
            {
                Text = value.ToString(),
                FontSize = 14,
                TextColor = (Color)App.Current.Resources["TextColor_5"],
                HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, false)
            };

            Stepper sp = new Stepper() { Value = value };
            lb.Text = value == 0 || value == Minimum ? ZeroText : value.ToString();
            sp.Increment = Increment;

            if (Maximum > 0)
                sp.Maximum = Maximum;

            if (Minimum > 0)
                sp.Minimum = Minimum;

            sp.ValueChanged += (s, e) =>
            {
                valueChanged?.Invoke(s, e);
                lb.Text = e.NewValue == 0 || e.NewValue == Minimum ? ZeroText : e.NewValue.ToString();
            };


            stkl.HorizontalOptions = new LayoutOptions(LayoutAlignment.End, true);
            stkl.VerticalOptions = new LayoutOptions(LayoutAlignment.Start, false);
            stkl.Children.Add(sp);
            stkl.Children.Add(lb);
            
            GenerateBaseControl(Title, Description, stkl, null);
        }


        /// <summary>
        /// Базовый контрол
        /// </summary>
        /// <param name="Title">Имя контрола</param>
        /// <param name="Description">Описание контрола</param>
        /// <param name="viewLeft">Контрол с лева</param>
        /// <param name="viewBottom">Контрол внизу</param>
        private void GenerateBaseControl(string Title, string Description, View viewLeft, View viewBottom)
        {
            StackLayout stackLayout = new StackLayout();


            // Титл
            title.Text = Title;
            title.FontSize = 18;
            title.SizeChanged += Label_SizeChanged;
            title.TextColor = (Color)App.Current.Resources["TextColor_5"];
            stackLayout.Children.Add(title);


            // Описание
            if (Description != null)
            {
                description.Text = Description;
                description.FontSize = 15;
                description.SizeChanged += Label_SizeChanged;
                description.TextColor = (Color)App.Current.Resources["TextColor_2"];
                stackLayout.Children.Add(description);
            }


            // Grid в котором находится "Титл,Описание,viewLeft"
            grid.Margin = new Thickness(0, 8, 0, 0);
            grid.Children.Add(stackLayout);
            if (viewLeft != null)
                grid.Children.Add(viewLeft);  // Боковой контрол


            // Добовляем грид в текущий StackLayout
            this.Children.Add(grid);


            // Нижний контрол
            if (viewBottom != null)
                this.Children.Add(viewBottom);


            // Линия разделитель
            boxView.HeightRequest = 1;
            boxView.BackgroundColor = (Color)App.Current.Resources["BackgroundColor_BoxViewLine_3"];
            boxView.HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
            this.Children.Add(boxView);


            if (viewLeft != null)
            {
                // Событие при изменение размера
                viewLeft.SizeChanged += (s, e) =>
                {
                    // Смешаем текст что-бы он не заходил за контрол
                    stackLayout.Margin = new Thickness(0, 0, viewLeft.Width + 12, 0);
                };
            }
        }


        /// <summary>
        /// Подгоняет высоту Grig по высоте текста
        /// </summary>
        private void Label_SizeChanged(object sender, EventArgs e)
        {
            double NextHeight = description.Height + title.Height + 10;

            //Правки под android
            if (Device.OS == TargetPlatform.Android)
                NextHeight = Math.Max(NextHeight, 70);

            // Размер блока
            if (grid.HeightRequest != NextHeight)
                grid.HeightRequest = NextHeight;
        }
    }
}
