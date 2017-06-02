using System;

namespace Freelancehunt_Messenger.sdk.Models.Profiles
{
    public class BaseProfile
    {
        public int profile_id { get; set; }
        public string avatar { get; set; }
        public bool is_freelancer { get; set; }
        public bool is_employer { get; set; }
        public string login { get; set; }
        public string fname { get; set; }
        public string sname { get; set; }
    }
}
