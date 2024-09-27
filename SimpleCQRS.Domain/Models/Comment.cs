using System;
using System.Collections.Generic;

namespace SimpleCQRS.Infrastructure;

public partial class Comment
{
    private Comment() { }

    public Guid CommentId { get; private set; }
    public string Text { get; private set; }
    public Guid PostId { get; private set; }
    public virtual Post Post { get; private set; } = null!;

    public static Comment CreateComment(Guid postId, string text)
    {
        return new Comment
        {
            CommentId = Guid.NewGuid(),
            PostId = postId,
            Text = text
        };
    }

    public void UpdateText(string newText)
    {
        Text = newText;
    }
}
