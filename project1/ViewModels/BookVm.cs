namespace project1.ViewModels
{
    public class BookVm
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        public DateTime PublishDate { get; set; }
        public string? imageUrl { get; set; }
        public List<string> Categories { get; set; }
    }
}
