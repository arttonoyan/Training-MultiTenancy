using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Training.MultiTenancy.Data.Entities;

namespace Training.MultiTenancy.Data.Infrastructure.EntityConfigurations;

internal class PostConfigurations : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {

    }
}
