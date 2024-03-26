namespace OnlineCasino.Pagination
{
    public class PaginationModel<T>
    {
        public IEnumerable<T> Paginate(IEnumerable<T> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public PaginationMetadata GetPaginationMetadata(int totalItems, int page, int pageSize)
        {
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            return new PaginationMetadata
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize
            };
        }
    }

    public class PaginationMetadata
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }

}
