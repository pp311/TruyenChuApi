using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebTruyenChu_Backend.Entities.EntityTypeConfiguration;

public class CommentReactionConfiguration : IEntityTypeConfiguration<CommentReaction>
{
    public void Configure(EntityTypeBuilder<CommentReaction> builder)
    {
        builder.HasKey(p => new { p.CommentId, p.UserId });
        
        builder.HasOne(cr => cr.Comment)
            .WithMany(c => c.CommentsReactions)
            .HasForeignKey(cr => cr.CommentId);

        builder.HasOne(cr => cr.User)
            .WithMany(u => u.CommentsReactions)
            .HasForeignKey(cr => cr.UserId);
    }
}