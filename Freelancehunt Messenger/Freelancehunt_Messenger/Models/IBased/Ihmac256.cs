namespace Freelancehunt_Messenger.Models.IBased
{
    public interface Ihmac256
    {
        string GetHash(string url, string method, string post_params, string api_secret);
    }
}
