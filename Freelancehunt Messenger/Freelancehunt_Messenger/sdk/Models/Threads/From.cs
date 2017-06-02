namespace Freelancehunt_Messenger.sdk.Models.Threads
{
    public class From
    {
        public int profile_id { get; set; }

        public string login { get; set; }

        public string fname { get; set; }

        public string sname { get; set; }

        public string avatar { get; set; }

        public string FullName
        {
            get
            {
                return fname + " " + sname;
            }
        }
    }
}
