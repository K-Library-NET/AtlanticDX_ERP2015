using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Privileges
{
    public enum EntityControlType
    {
        OrderContract,
        SaleContract,
    }

    public class PrivilegeLevelByEntityControlType
    {
        public const int DEFAULT_LEVEL_ORDER_CONTRACT = 50;
        public const int DEFAULT_LEVEL_SALE_CONTRACT = 50;

        public static int GetDefaultPrivilegeLevelByEntityControlType(EntityControlType type)
        {
            if (type == EntityControlType.OrderContract)
                return DEFAULT_LEVEL_ORDER_CONTRACT;
            else if (type == EntityControlType.SaleContract)
                return DEFAULT_LEVEL_SALE_CONTRACT;
            return 50;
        }
    }
}
