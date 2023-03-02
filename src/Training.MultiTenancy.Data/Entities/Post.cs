using Training.MultiTenancy.Data.Entities.Infrastructure;

namespace Training.MultiTenancy.Data.Entities;

public class Post : IBaseEntity
{
    public int PostId { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public bool IsDeleted { get; set; }

    public Blog Blog { get; set; } = null!;
}
