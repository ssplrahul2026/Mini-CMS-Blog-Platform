namespace MiniCMS.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalPosts { get; set; }

        public int TotalCategories { get; set; }

        public int TotalTags { get; set; }

        public int TotalComments { get; set; }

        public int PublishedPosts { get; set; }

        public int DraftPosts { get; set; }
    }
}