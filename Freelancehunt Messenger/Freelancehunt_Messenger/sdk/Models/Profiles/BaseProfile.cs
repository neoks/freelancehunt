using System;

namespace Freelancehunt_Messenger.sdk.Models.Profiles
{
    public class BaseProfile
    {
        public int profile_id { get; set; }
        public string url { get; set; }
        public string url_private_message { get; set; }
        public string avatar { get; set; }
        public string avatar_md { get; set; }
        public bool is_freelancer { get; set; }
        public bool is_employer { get; set; }
        public string login { get; set; }
        public string fname { get; set; }
        public string sname { get; set; }
        public string birth_date { get; set; }
        public int rating { get; set; }
        public int rating_position { get; set; }
        public string country_name_ru { get; set; }
        public string city_name_ru { get; set; }
        public DateTime creation_date { get; set; }
        public string cv { get; set; }
        public string cv_html { get; set; }
        public int positive_reviews { get; set; }
        public int negative_reviews { get; set; }
        public string is_phone_verified { get; set; }
        public string is_fname_verified { get; set; }
        public string is_birth_date_verified { get; set; }
        public string is_wmid_verified { get; set; }
        public string is_okpay_verified { get; set; }
        public string is_email_verified { get; set; }
        public bool is_online { get; set; }
        public bool is_plus_active { get; set; }
        public DateTime last_activity { get; set; }
        public string website { get; set; }
    }
}
