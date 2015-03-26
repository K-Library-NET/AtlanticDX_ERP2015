using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.Sale;

namespace PrivilegeFramework.PrivilegeFilters
{
    internal interface ISaleContractQueryBuilder
    {
        IQueryable<SaleContract> BuildQueryable(ExtendedIdentityDbContext db,
            IQueryable<SaleContract> sourceQuery);
    }
}
