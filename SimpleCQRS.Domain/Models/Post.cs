using System;
using System.Collections.Generic;

namespace SimpleCQRS.Infrastructure;

public partial class Post
{
    private readonly List<Comment> _comments = new();

    public Post()
    {

    }
    public Guid PostId { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public DateTime DateCreated { get; private set; }
    public DateTime LastModified { get; private set; }

    public IEnumerable<Comment> Comments => _comments.AsReadOnly();//public IEnumerable<Comment> Comments { get { return _comment;} }
    //public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public static Post CreatePost(string title, string content)
    {
        var post = new Post
        {
            PostId = Guid.NewGuid(),
            Title = title,
            Content = content,
            DateCreated = DateTime.UtcNow,
            LastModified = DateTime.UtcNow
        };

        return post;
    }

    public void UpdatePost(string title, string content)
    {
        Title = title;
        Content = content;
        LastModified = DateTime.UtcNow;
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public void RemoveComment(Comment comment)
    {
        _comments.Remove(comment);
    }
}
