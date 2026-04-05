namespace TravelGuide.ViewModels
{
    public class PaginationViewModel
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public string? Action { get; set; }
        public string? Controller { get; set; }
        public Dictionary<string, string>? RouteValues { get; set; }
    }

    public class PaginatedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalItems { get; private set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public PaginatedList(List<T> items, int count, int currentPage, int pageSize)
        {
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalItems = count;
            AddRange(items);
        }

        public static PaginatedList<T> Create(IQueryable<T> source, int currentPage, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, currentPage, pageSize);
        }
    }
}