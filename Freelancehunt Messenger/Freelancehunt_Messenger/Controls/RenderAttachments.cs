using Freelancehunt_Messenger.sdk.Models.Threads;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Freelancehunt_Messenger.Controls
{
    public class RenderAttachments : WrapLayout
    {
        public RenderAttachments()
        {
            this.PropertyChanged += RenderAttachments_PropertyChanged;
        }

        bool IsRenderer = false;
        public static readonly BindableProperty AttachmentsProperty = BindableProperty.Create(propertyName: "AttachmentsProperty", returnType: typeof(List<Attachment>), declaringType: typeof(List<Attachment>), defaultValue: null);

        public List<Attachment> Attachments
        {
            get { return (List<Attachment>)GetValue(AttachmentsProperty); }
            set { SetValue(AttachmentsProperty, value); }
        }


        private void RenderAttachments_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (IsRenderer || Attachments == null)
                return;

            // Старт 
            IsRenderer = true;
            this.Orientation = StackOrientation.Horizontal;
            this.VerticalOptions = LayoutOptions.Start;
            this.HorizontalOptions = LayoutOptions.Start;
            this.Padding = new Thickness(0, 5, 0, 0);
            this.Spacing = 10;


            foreach (var attach in Attachments)
            {
                // Контейнер
                StackLayout st = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.Start,
                };


                // Иконка
                st.Children.Add(new Image()
                {
                    Source = Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone ? "img/icon/file.png" : "file.png",
                    HeightRequest = 30,
                    WidthRequest = 30,
                    MinimumHeightRequest = 30,
                    MinimumWidthRequest = 30
                });


                // Имя и тип файла
                st.Children.Add(new StackLayout()
                {
                    VerticalOptions = LayoutOptions.Start,
                    Spacing = 0,
                    Padding = 0,

                    Children = {
                        new Label()
                        {
                            Text = attach.FileName,
                            LineBreakMode = LineBreakMode.TailTruncation,
                            TextColor = Color.FromHex("#464646"),
                            FontSize = 13,
                        },
                        new Label()
                        {
                            Text =  attach.file_type,
                            FontSize = 11,
                            TextColor = Color.FromHex("#c5c5c5")
                        }
                    }
                });


                // Переход по ссылке
                TapGestureRecognizer tap = new TapGestureRecognizer();
                tap.Tapped += (s, et) =>
                {
                    Device.OpenUri(new Uri(attach.url));
                };
                st.GestureRecognizers.Add(tap);

                // Вывод на экран
                this.Children.Add(st);
            }
        }
    }
}
