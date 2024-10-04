using EventTracker.Dtos;
using Microsoft.AspNetCore.SignalR.Client;
using SimpleCQRS.EventTracker.Dtos;

namespace EventTracker.Services
{
    public class SignalRService : ISignalRService
    {
        /// <summary>
        /// Hub Connection
        /// </summary>
        private readonly HubConnection _connection;


        public SignalRService(string URL)
        {
            _connection = new HubConnectionBuilder()
           .WithUrl(URL)
           .Build();
        }

        public async Task StartAsync()
        {
            await _connection.StartAsync();
        }


        public void OnPostCreated(Action<GetPostDto> handler)
        {
            _connection.On("PostCreated", handler);
        }

        public void OnPostUpdated(Action<GetPostDto> handler)
        {
            _connection.On("PostUpdated", handler);
        }

        public void OnPostDeleted(Action<GetPostDto> handler)
        {
            _connection.On("PostDeleted", handler);
        }

        public void OnPostsRetrieved(Action<List<GetPostDto>> handler)
        {
            _connection.On("PostsRetrieved", handler);
        }

        public void OnPostRetrieved(Action<GetPostDto> handler)
        {
            _connection.On("PostGet", handler);
        }

        public void OnCommentCreated(Action<GetCommentDto> handler)
        {
            _connection.On("CommentCreated", handler);
        }
    }
}
