using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities
{
    public class ContactPersonInfo : YuShang.ERP.Entities.IContactPersonInfo
    {
        public string MobilePhone
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public string QQ_or_WeChat
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}
