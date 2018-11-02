namespace PartyInvites.Models
{
    using System.Collections.Generic;

    public static class Repository
    {
        private static readonly List<GuestResponse> responses = new List<GuestResponse>();

        public static IEnumerable<GuestResponse> Responses => responses;

        public static void AddResponse(GuestResponse response)
        {
            responses.Add(response);
        }
    }
}