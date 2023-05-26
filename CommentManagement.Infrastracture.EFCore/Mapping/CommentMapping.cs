using Microsoft.EntityFrameworkCore;
using CommentManagement.Domain.CommentAgg;

namespace CommentManagement.Infrastracture.EFCore.Mapping;
public class CommentMapping : IEntityTypeConfiguration<Comment>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(500);

        builder.Property(x => x.Message)
            .HasMaxLength(1000);

        builder.Property(x => x.Email)
            .HasMaxLength(500);

        builder.Property(x => x.Website)
            .HasMaxLength(500);
    }
}
