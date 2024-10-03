using EventTracker.Consts;

namespace EventTracker.Dtos
{
    public class EventTracker
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public MyHttpMethod Types { get; set; }
    }
}
