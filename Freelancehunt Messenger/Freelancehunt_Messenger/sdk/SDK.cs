using System;
using System.Threading.Tasks;
using Freelancehunt_Messenger.sdk.Models.Profiles;
using System.Collections.Generic;
using Freelancehunt_Messenger.sdk.Models.Threads;
using Freelancehunt_Messenger.sdk.Models.MY;
using Newtonsoft.Json;

namespace Freelancehunt_Messenger.sdk
{
    public class SDK : DefaultRestClient
    {
        public class my : DefaultRestClient
        {
            public Task<List<feed>> Feed
            {
                get
                {
                    return GetRequest<List<feed>>("my/feed");
                }
            }
        }


        public class Profiles : DefaultRestClient
        {
            public Task<ME> me
            {
                get
                {
                    return GetRequest<ME>("profiles/me");
                }
            }

            public Task<BaseProfile> Show(string login)
            {
                return GetRequest<BaseProfile>($"profiles/{login}");
            }
        }


        public class Threads : DefaultRestClient
        {
            public Task<List<MessageMain>> Show(short page, short per_page)
            {
                return GetRequest<List<MessageMain>>($"threads?page={page}&per_page={per_page}");
            }

            public Task<List<MessageMain>> Show(string filter)
            {
                return GetRequest<List<MessageMain>>($"threads?filter={filter}");
            }

            public Task<List<MessageThread>> Show(int thread_id)
            {
                return GetRequest<List<MessageThread>>($"threads/{thread_id}");
            }

            async public Task<MessageThread> Send(int thread_id, string msg)
            {
                string Response = await PostRequest($"threads/{thread_id}", new SendMSG() { message = msg });
                return JsonConvert.DeserializeObject<MessageThread>(Response);
            }
        }




        public Task<object> Test()
        {
            return GetRequest<object>($"threads?page=1&per_page=15&filter=new");
        }
    }
}
