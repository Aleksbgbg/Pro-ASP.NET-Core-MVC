﻿namespace UrlsAndRouting.Models
{
    using System.Collections.Generic;

    public class Target
    {
        public Target(string controller, string method)
        {
            Controller = controller;
            Method = method;
        }

        public string Controller { get; }

        public string Method { get; }

        public Dictionary<string, object> Data { get; } = new Dictionary<string, object>();
    }
}