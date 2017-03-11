using System;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Xamarin.Forms;

namespace Freelancehunt_Messenger.Windows.engine
{
    public class HMAC256 : Models.IBased.Ihmac256
    {
        public string GetHash(string url, string method, string post_params, string api_secret)
        {
            var algorithmProvider = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha256);
            var contentBuffer = CryptographicBuffer.ConvertStringToBinary(url + method + post_params, BinaryStringEncoding.Utf8);
            var keyBuffer = CryptographicBuffer.ConvertStringToBinary(api_secret, BinaryStringEncoding.Utf8);
            var signatureKey = algorithmProvider.CreateKey(keyBuffer);
            var signedBuffer = CryptographicEngine.Sign(signatureKey, contentBuffer);
            return CryptographicBuffer.EncodeToBase64String(signedBuffer);
        }
    }
}
