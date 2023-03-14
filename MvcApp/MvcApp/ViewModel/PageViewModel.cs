namespace MvcApp.ViewModel
{
    public class PageViewModel
    {
        public int PageNumber { get; }
        public int TotalPages { get; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public PageViewModel(int count, int pageNuber, int pageSize)
        {
            PageNumber = pageNuber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
