namespace SportsStore.Extensions
{
    using Microsoft.AspNetCore.Http;

    public static class HttpRequestExtensions
    {
        public static string PathAndQuery(this HttpRequest request)
        {
            return request.QueryString.HasValue ? string.Concat(request.Path, request.QueryString) : request.Path.ToString();
        }
    }
}