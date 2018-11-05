namespace SportsStore.Models
{
    using System;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    using Newtonsoft.Json;

    using SportsStore.Extensions;

    public class SessionCart : Cart
    {
        [JsonIgnore]
        private ISession Session { get; set; }

        public static SessionCart Make(IServiceProvider serviceProvider)
        {
            ISession session = serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;

            SessionCart sessionCart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            sessionCart.Session = session;

            return sessionCart;
        }

        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            Session.SetJson("Cart", this);
        }

        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            Session.SetJson("Cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
    }
}