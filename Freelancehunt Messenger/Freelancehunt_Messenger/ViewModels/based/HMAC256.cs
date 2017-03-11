using System;
using Freelancehunt_Messenger.Models.IBased;
using Xamarin.Forms;

namespace Freelancehunt_Messenger.ViewModels.based
{
    internal static class HMAC256
    {
        static Ihmac256 hmac;

        static HMAC256()
        {
            hmac = DependencyService.Get<Ihmac256>();
        }


        internal static string GetHash(string url, string method, string post_params, string api_secret)
        {
            return hmac.GetHash(url, method, post_params, api_secret);
        }
    }
}
