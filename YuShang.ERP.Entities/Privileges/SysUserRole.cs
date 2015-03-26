using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Privileges
{
    public class SysUserRole : IdentityUserRole<int>
    {
        public SysUserRole()
        {
        }

        [System.ComponentModel.DataAnnotations.ConcurrencyCheck]
        public override int RoleId
        {
            get
            {
                return base.RoleId;
            }
            set
            {
                base.RoleId = value;
            }
        }

        [System.ComponentModel.DataAnnotations.ConcurrencyCheck]
        public override int UserId
        {
            get
            {
                return base.UserId;
            }
            set
            {
                base.UserId = value;
            }
        }
    }
}
