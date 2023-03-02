using Microsoft.EntityFrameworkCore;
using Training.MultiTenancy.Data.Entities;

namespace Training.MultiTenancy.Data;

//https://learn.microsoft.com/en-us/ef/core/querying/filters
public partial class TenantDbCntext : DbContext
{
    private readonly TenantInfo _tenantInfo;

    public TenantDbCntext(DbContextOptions<TenantDbCntext> options, TenantInfo tenantInfo)
    : base(options)
    {
        _tenantInfo = tenantInfo;
    }

    public virtual DbSet<Blog> Blogs { get; set; } = null!;
    public virtual DbSet<Post> Posts { get; set; } = null!;
}
