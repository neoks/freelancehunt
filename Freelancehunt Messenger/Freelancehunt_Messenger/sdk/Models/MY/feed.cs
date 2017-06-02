using System;
using Xamarin.Forms;

namespace Freelancehunt_Messenger.sdk.Models.MY
{
    public class From
    {
        public string login { get; set; }
        public string url { get; set; }
        public string avatar { get; set; }
    }

    public class Related
    {
        public string project_id { get; set; }
    }

    public class feed
    {
        public From from { get; set; }
        public DateTime time { get; set; }
        public string message { get; set; }
        public bool is_new { get; set; }
        public Related related { get; set; }

        public bool IsPhone => Device.Idiom == TargetIdiom.Phone;
    }
}
