using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(Freelancehunt_Messenger.iOS.engine.HMAC256))]
namespace Freelancehunt_Messenger.iOS.engine
{
    public class HMAC256 : Models.IBased.Ihmac256
    {
        public string GetHash(string url, string method, string post_params, string api_secret)
        {
            byte[] keyByte = Encoding.UTF8.GetBytes(api_secret);
            byte[] messageBytes = Encoding.UTF8.GetBytes(url + method + post_params);

            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }
    }
}
