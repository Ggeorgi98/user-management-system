namespace UserManagementSystem.Business.Models
{
    public class PagerModel
    {
        public int CurrentPageNumber { get; set; }

        public int TotalPages { get; set; }

        public int ItemsPerPage { get; set; }

        public int TotalItemsCount { get; set; }
    }
}
