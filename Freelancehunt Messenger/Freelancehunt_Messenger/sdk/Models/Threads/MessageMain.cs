using System;

namespace Freelancehunt_Messenger.sdk.Models.Threads
{
    public class MessageMain
    {
        private string _subject;

        public int thread_id { get; set; }

        public string subject
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_subject))
                    return "Без темы";

                if (_subject != null && _subject.Contains("Рабочая область проекта"))
                    return _subject.Replace("Рабочая область проекта", "Р/П:");

                return _subject;
            }
            set
            {
                _subject = value;
            }
        }

        public string url { get; set; }

        public string url_api { get; set; }

        public From from { get; set; }

        public string has_attach { get; set; }

        public short message_count { get; set; }

        public DateTime first_post_time { get; set; }

        public DateTime last_post_time { get; set; }

        public object is_unread { get; set; }

        public bool IsUnread
        {
            get
            {
                return (is_unread is bool && (bool)is_unread);
            }
        }

        public Xamarin.Forms.Color BackgroundColor { get; set; }
    }
}
