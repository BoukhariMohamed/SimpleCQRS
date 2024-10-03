using EventTracker.Dtos;

namespace EventTracker.Services;

public interface ISignalRService
{
    /// <summary>
    /// Starts the SignalR connection asynchronously
    /// </summary>
    Task StartAsync();

    /// <summary>
    /// Subscribes to the PostCreated event
    /// </summary>
    /// <param name="handler">Action to execute when PostCreated event is received</param>
    void OnPostCreated(Action<GetPostDto> handler);

    /// <summary>
    /// Subscribes to the PostUpdated event
    /// </summary>
    /// <param name="handler">Action to execute when PostUpdated event is received</param>
    void OnPostUpdated(Action<GetPostDto> handler);

    /// <summary>
    /// Subscribes to the PostDeleted event
    /// </summary>
    /// <param name="handler">Action to execute when PostDeleted event is received</param>
    void OnPostDeleted(Action<GetPostDto> handler);

    /// <summary>
    /// Subscribes to the PostsRetrieved event
    /// </summary>
    /// <param name="handler">Action to execute when PostsRetrieved event is received</param>
    void OnPostsRetrieved(Action<List<GetPostDto>> handler);

    /// <summary>
    /// Subscribes to the PostRetrieved event
    /// </summary>
    /// <param name="handler">Action to execute when a single post is retrieved</param>
    void OnPostRetrieved(Action<GetPostDto> handler);
}
