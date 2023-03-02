using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Training.MultiTenancy.Data.Entities;

namespace Training.MultiTenancy.Data.Infrastructure.EntityConfigurations;

internal class BlogConfigurations : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.HasMany(b => b.Posts).WithOne(p => p.Blog);
        builder.HasQueryFilter(b => b.Posts.Count > 0);
    }
}
