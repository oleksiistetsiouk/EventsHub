using System;

namespace EventsHub.Common.Helpers
{
    public class FilterParams
    {
        private int _pageNumber = 1;
        private int _pageSize = 1;

        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value > 1 ? value : 1;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > 0 ? value : 0;
        }

        public int Skip => (PageNumber - 1) * PageSize;
        public int Take => PageSize;
        public int FromPrice { get; set; } = 0;
        public int? ToPrice { get; set; } = Int32.MaxValue;
        public DateTime FromDate { get; set; } = DateTime.Now;
        public DateTime? ToDate { get; set; } = DateTime.MaxValue;
    }
}
