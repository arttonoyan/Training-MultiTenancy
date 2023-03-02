using Training.MultiTenancy.Data.Entities.Infrastructure;

namespace Training.MultiTenancy.Data.Entities;

public class Blog : ITenantBaseEntity, IBaseEntity
{
    public int TenantId { get; set; }
    public int BlogId { get; set; }
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
    public bool IsDeleted { get; set; }

    public List<Post> Posts { get; set; } = null!;
}
