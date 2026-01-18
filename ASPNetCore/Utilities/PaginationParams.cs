namespace ASPNetCore.Utilities
{
    public class PaginationParams
    {
        private int _page = 1;
        private int _pageSize = 10;

        public int Page
        {
            get => _page;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(Page), "Page must be a positive integer.");
                _page = value;
            }
        }

        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(PageSize), "PageSize must be a positive integer.");
                _pageSize = value;
            }
        }

        public PaginationParams() { }

        public PaginationParams(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }

}
