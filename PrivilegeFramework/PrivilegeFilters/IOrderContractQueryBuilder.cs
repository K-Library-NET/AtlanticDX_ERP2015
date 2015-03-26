using System;
using System.Linq;
using YuShang.ERP.Entities.Orders;
namespace PrivilegeFramework.PrivilegeFilters
{
    internal interface IOrderContractQueryBuilder
    {
        System.Linq.IQueryable<YuShang.ERP.Entities.Orders.OrderContract> BuildQueryable(
            ExtendedIdentityDbContext db, IQueryable<OrderContract> sourceQuery);
    }
}
