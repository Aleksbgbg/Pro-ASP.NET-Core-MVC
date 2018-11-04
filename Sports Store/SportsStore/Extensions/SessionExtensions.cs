namespace SportsStore.Extensions
{
    using Microsoft.AspNetCore.Http;

    using Newtonsoft.Json;

    public static class SessionExtensions
    {
        public static T GetJson<T>(this ISession session, string key)
        {
            string sessionData = session.GetString(key);

            return sessionData == null ? default : JsonConvert.DeserializeObject<T>(sessionData);
        }

        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }
}