using System;
namespace YuShang.ERP.Entities
{
    public interface IContactPersonInfo
    {
        string Address { get; set; }
        string MobilePhone { get; set; }
        string Name { get; set; }
        string QQ_or_WeChat { get; set; }
    }
}
