
using Microsoft.AspNetCore.SignalR.Client;
namespace SignalRClient;

public class Program
{
    public static async Task Main(string[] args)
    {
        // Set up the connection to your SignalR hub
        var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5222/notificationhub")
            .Build();

        // Set up the event handler for receiving post deletions
        connection.On<Guid>("PostDeleted", (postId) =>
        {
            Console.WriteLine($"Post deleted: {postId}");
        });

        // Set up the event handler for receiving all posts
        connection.On<List<GetPostDto>>("PostsRetrieved", (posts) =>
        {
            Console.WriteLine("All posts retrieved:");
            foreach (var post in posts)
            {
                Console.WriteLine($"Post ID: {post.PostId}, Title: {post.Title}, Content: {post.Content}");
            }
        });


        // Start the connection
        try
        {
            await connection.StartAsync();
            Console.WriteLine("Connection started. Listening for messages...");

            // Prevent the application from exiting immediately
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection failed: {ex.Message}");
        }
        finally
        {
            // Dispose the connection when done
            await connection.DisposeAsync();
        }
    }
}
public class GetPostDto
{
    public Guid PostId { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public DateTime DateCreated { get; private set; }
    public DateTime LastModified { get; private set; }
}