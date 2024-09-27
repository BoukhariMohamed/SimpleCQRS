using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SimpleCQRS.Infrastructure;

namespace SimpleCQRS.Infrastructure.Data;

public partial class SimpleCqrsContext : DbContext
{
    public SimpleCqrsContext()
    {
    }

    public SimpleCqrsContext(DbContextOptions<SimpleCqrsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__C3B4DFCA8C217420");

            entity.Property(e => e.CommentId).ValueGeneratedNever();

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_Comments_Posts");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Posts__AA12601870D5D293");

            entity.Property(e => e.PostId).ValueGeneratedNever();
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
