namespace UrlsAndRouting.Constraints
{
    using System;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class ExactValueConstraint : IRouteConstraint
    {
        private readonly string _value;

        public ExactValueConstraint(string value)
        {
            _value = value;
        }

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return values[routeKey]?.ToString() == _value;
        }
    }
}