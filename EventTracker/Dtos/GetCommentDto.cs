namespace SimpleCQRS.EventTracker.Dtos
{
    public class GetCommentDto
    {
        public Guid CommentId { get; set; }
        public string Text { get; set; }
        public Guid PostId { get; set; }
    }
}
