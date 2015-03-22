using System;
namespace PrivilegeFramework.OrderRelateds
{
    public interface IContractRelatedViewModel
    {
        bool? IsEnable { get; set; }

        string ErrorMessage { get; set; }
    }
}
