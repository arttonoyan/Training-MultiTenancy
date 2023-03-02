using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Security.Principal;
using Training.MultiTenancy.Data.Entities.Infrastructure;
using Training.MultiTenancy.Data.Infrastructure.EntityConfigurations;

namespace Training.MultiTenancy.Data;

partial class TenantDbCntext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogConfigurations).Assembly);
        OnModelCreatingPartial(modelBuilder);

        //foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
        //{
        //    if (mutableEntityType.ClrType.IsAssignableTo(typeof(ITenantBaseEntity)))
        //    {
        //        Expression<Func<ITenantBaseEntity, bool>> filterExpr = e => e.TenantId == _tenantInfo.TenantId;

        //        if (mutableEntityType.ClrType.IsAssignableTo(typeof(IBaseEntity)))
        //        {
        //            filterExpr = filterExpr.And<IBaseEntity>(entity => !((IEntity)entity));
        //        }

        //        var parameter = Expression.Parameter(mutableEntityType.ClrType);
        //        var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
        //        var lambdaExpression = Expression.Lambda(body, parameter);
        //        mutableEntityType.SetQueryFilter(lambdaExpression);
        //    }
        //}
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
