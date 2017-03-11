using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Freelancehunt_Messenger.Controls
{
    public class RenderText : StackLayout
    {
        public RenderText()
        {
            this.Orientation = StackOrientation.Vertical;
            this.VerticalOptions = LayoutOptions.Start;
            this.HorizontalOptions = LayoutOptions.Start;
            this.Padding = 0;
            this.Spacing = 0;
            this.PropertyChanged += RenderText_PropertyChanged;  
        }

        bool IsRenderer = false;
        public static readonly BindableProperty MessageHtmlProperty = BindableProperty.Create(propertyName: "MessageHtml", returnType: typeof(string), declaringType: typeof(string), defaultValue: null);

        public string MessageHtml
        {
            get { return (string)GetValue(MessageHtmlProperty); }
            set { SetValue(MessageHtmlProperty, value); }
        }
        

        void RenderText_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (IsRenderer || MessageHtml == null)
                return;
            IsRenderer = true;

            // Старт 
            string SplitBbCode = "_SplitBbCode_";
            Color textColor = Color.FromHex("#464646");

            #region Начальный текст - (вырезаем весь код форматирования текста и заменяем часть кода на свой)
            string message_html = Regex.Replace(MessageHtml, "(<br />|<a [^>]+>([^<]+|<[^<]+>|<[a-z] +[^<]+</[a-z]>)</a>|<img [^>]+>|<[^>]+>|\\&lt;|\\&gt;)", rg =>
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
                    case "<br />": return "<br />";

                    case "<em>": return $"{SplitBbCode}em:";
                    case "<strong>": return $"{SplitBbCode}strong:";

                    case "</em>":
                    case "</strong>": return $"{SplitBbCode}";
                }

                // Текста в <h*>*</h*>
                if (Regex.IsMatch(rg.Groups[0].Value, "</h[0-9]+>"))
                    return "<br />";

                // Ссылка
                if (rg.Groups[0].Value.Contains("href="))
                {
                    var gLink = new Regex("href=\"([^\"]+)\"([^>]+)?>(.*)</a>").Match(rg.Groups[0].Value).Groups;

                    string title = gLink[3].Value;
                    title = Regex.Replace(title, "<i +class=\"fa +fa-([^\"]+)\"></i>", "$1");
                    title = Regex.Replace(title, "<img +src=\"/static/images/fugu/balloon(-|_)([a-z]+)\\..*", "$2");
                    return $"{SplitBbCode}link:{gLink[1].Value},{title}{SplitBbCode}";
                }

                // Изображение
                if (rg.Groups[0].Value.Contains("src="))
                {
                    return $"{SplitBbCode}img:{new Regex("src=\"([^\"]+)\"").Match(rg.Groups[0].Value).Groups[1].Value}{SplitBbCode}";
                }

                // Удаляем
                return "";
            });
            #endregion

            // Дебаг
            //Debug.WriteLine(MessageHtml);

            #region Форматирования текста
            bool IsNewLineTwo = false;
            var MassItem = Regex.Split(message_html, "<br />");
            for (int i = 0; i < MassItem.Length; i++)
            {
                // Достаем строку
                string line = MassItem[i];

                // Максимум два \n
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (IsNewLineTwo || i == MassItem.Length - 1 || string.IsNullOrWhiteSpace(MassItem[i + 1]))
                        continue;

                    IsNewLineTwo = true;
                    this.Children.Add(new Label());
                    continue;
                }
                else { IsNewLineTwo = false; }

                // Костыльный StackLayout
                WrapLayout wrapLayout = new WrapLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.Start,
                    Padding = 0,
                    Spacing = 5
                };


                #region Формируем текст
                foreach (var splitLine in Regex.Split(line, SplitBbCode))
                {
                    if (string.IsNullOrWhiteSpace(splitLine))
                        continue;

                    // BB code
                    if (Regex.IsMatch(splitLine, "^(em|strong|link|img):"))
                    {
                        var grp = new Regex("^(em|strong|link|img):(.*)$").Match(splitLine).Groups;
                        if (string.IsNullOrWhiteSpace(grp[2].Value))
                            continue;

                        // Стили для текста
                        switch (grp[1].Value)
                        {
                            case "em":
                            case "strong":
                                {
                                    FormatLabelTextToWrapLayout(wrapLayout, grp[2].Value, textColor, grp[1].Value == "em" ? FontAttributes.Italic : FontAttributes.Bold);
                                    break;
                                }

                            case "img":
                                {
                                    string url = grp[2].Value.Split(',')[0];
                                    if (!url.Contains("/static/images/fugu/"))
                                        goto case "link";

                                    wrapLayout.Children.Add(new Image()
                                    {
                                        Source = url.Contains("freelancehunt.com") ? url : $"https://freelancehunt.com/{url}",
                                        HeightRequest = 17,
                                        WidthRequest = 17,
                                        MinimumHeightRequest = 17,
                                        MinimumWidthRequest = 17,
                                        Margin = new Thickness(0, 4, 0, 0),
                                        HorizontalOptions = LayoutOptions.Start,
                                        VerticalOptions = LayoutOptions.Start,
                                    });
                                    break;
                                }
                            case "link":
                                {
                                    TapGestureRecognizer tap = new TapGestureRecognizer();
                                    tap.Tapped += (s, et) =>
                                    {
                                        string url = grp[2].Value.Split(',')[0];
                                        if (!Regex.IsMatch(url, "^https?://"))
                                            url = "https://freelancehunt.com/" + Regex.Replace(url, "^/", "");

                                        Device.OpenUri(new Uri(url));
                                    };

                                    FormatLabelTextToWrapLayout(wrapLayout, (grp[1].Value == "img" ? "изображение" : grp[2].Value.Split(',')[1]), Color.FromHex("#6ea6eb"), lineBreakMode: LineBreakMode.TailTruncation, tap: tap);
                                    break;
                                }
                        }
                    }

                    // Обычный текст
                    else
                    {
                        FormatLabelTextToWrapLayout(wrapLayout, splitLine, textColor);
                    }
                }

                this.Children.Add(wrapLayout);
                #endregion
            }
            #endregion
        }



        void FormatLabelTextToWrapLayout(WrapLayout wrapLayout, string text, Color color, FontAttributes fontAttributes = FontAttributes.None, LineBreakMode lineBreakMode = LineBreakMode.NoWrap, TapGestureRecognizer tap = null)
        {
            foreach (var line in text.Trim().Split(' '))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                Label lb = new Label()
                {
                    FontAttributes = fontAttributes,
                    LineBreakMode = lineBreakMode,
                    FontSize = PlatformInvoke.IsWindows ? 15 : 16,
                    TextColor = color,
                    Text = line
                };

                if (tap != null)
                    lb.GestureRecognizers.Add(tap);

                wrapLayout.Children.Add(lb);
            }
        }
    }
}
