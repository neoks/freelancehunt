using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Freelancehunt_Messenger.ViewModels.based;
using Newtonsoft.Json;
using Freelancehunt_Messenger.Models.IBased;
using Xamarin.Forms;
using Freelancehunt_Messenger.Models.based;
using System.Diagnostics;

namespace Freelancehunt_Messenger.sdk
{
    public class DefaultRestClient
    {
        static string MyUserAgent = $"Freelancehunt Mobile / (ver: 1.3.*; OS: {Device.OS.ToString()}; Idiom: {Device.Idiom.ToString()}; Author: neoks;)";
        static string DefaultUrlAPI = "https://api.freelancehunt.com/";
        public static string apiSecret, apiToken;


        protected async Task<T> GetRequest<T>(string resourse)
        {
            string hmacHash = HMAC256.GetHash(DefaultUrlAPI + resourse, Method.GET.ToString(), "", apiSecret);

            using (var client = new RestClient(DefaultUrlAPI))
            {
                client.UserAgent = MyUserAgent;

                var request = new RestRequest(resourse, Method.GET);
                request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(apiToken + ":" + hmacHash)));

                IRestResponse <T> IRest = await client.Execute<T>(request);
                return IRest.Data;
            }
        }
        

        protected async Task<string> PostRequest(string resourse, object ob)
        {
            string param = JsonConvert.SerializeObject(ob).ToString();
            string hmacHash = HMAC256.GetHash(DefaultUrlAPI + resourse, Method.POST.ToString(), param, apiSecret);

            using (var client = new HttpClient())
            {
                var url = DefaultUrlAPI + resourse;
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", MyUserAgent);
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(apiToken + ":" + hmacHash)));
                HttpContent content = new StringContent(param, Encoding.UTF8, "application/json");

                var resp = await client.PostAsync(url, content);
                if (resp.IsSuccessStatusCode)
                {
                    return await resp.Content.ReadAsStringAsync();
                }
                
                return null;
            }
        }
    }
}
