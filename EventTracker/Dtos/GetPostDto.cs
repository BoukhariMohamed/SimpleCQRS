namespace EventTracker.Dtos
{
    public class GetPostDto
    {
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
    }
}
