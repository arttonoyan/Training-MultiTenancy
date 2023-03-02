using Training.MultiTenancy.Data.Entities.Infrastructure;

namespace Training.MultiTenancy.Data.Entities;

public class Blog : ITenantBaseEntity
{
    public int TenantId { get; set; }
    public int BlogId { get; set; }
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;

    public List<Post> Posts { get; set; } = null!;
}
