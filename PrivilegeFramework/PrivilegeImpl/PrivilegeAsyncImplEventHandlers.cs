using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivilegeFramework.PrivilegeImpl
{
    internal delegate IdentityResult CoreActionEventHandler(object[] paras);

    internal delegate string BuildErrorFormatStringEventHandler(object[] paras);

    internal delegate string BuildDbUpdateConcurrencyExceptionFormatStringEventHandler(
        object[] paras, DbUpdateConcurrencyException dbException);
}
