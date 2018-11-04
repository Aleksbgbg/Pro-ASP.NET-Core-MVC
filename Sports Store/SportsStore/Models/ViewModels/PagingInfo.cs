namespace SportsStore.Models.ViewModels
{
    using System;

    public class PagingInfo
    {
        public int ItemCount { get; set; }

        public int ItemsPerPage { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)ItemCount / ItemsPerPage);
    }
}