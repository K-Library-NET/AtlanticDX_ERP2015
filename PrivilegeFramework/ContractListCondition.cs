using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivilegeFramework
{
    /// <summary>
    /// 通过ContractManager获取的列表所使用的过滤信息
    /// </summary>
    public class ContractListCondition
    {
        /// <summary>
        /// 是否处于有效状态：
        /// 如果IsEnable==false，那么就是不使用条件
        /// </summary>
        [Display(Name = "是否处于有效状态")]
        //[Required]
        public bool? IsEnable
        {
            get;
            set;
        }

        /// <summary>
        /// 返回的列表需要包含的对象
        /// </summary>
        [Display(Name = "返回的列表需要包含的对象")]
        //[Required]
        public ContractListInclude ListInclude
        {
            get;
            set;
        }

        /// <summary>
        /// 当前页数，如果是空则视为不分页
        /// </summary>
        [Display(Name = "当前页数")]
        public int? Page
        {
            get;
            set;
        }

        /// <summary>
        /// 每页行数，如果为空则视为不分页
        /// </summary>
        [Display(Name = "每页行数")]
        public int? Rows
        {
            get;
            set;
        }

        /// <summary>
        /// 当前用户的用户名，空则表示不根据权限过滤。
        /// 某些用户名只有一边的功能权限（采购或销售），则自动只返回该类数据
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 0是期货，1是现货
        /// 如果此字段为空，则不过滤期货还是现货
        /// </summary>
        [Display(Name = "期货/现货")]
        public int? OrderType { get; set; }

        /// <summary>
        /// 订单（包括采购、销售）创建时间过滤的“左端点”，使用闭区间，就是>=
        /// </summary>
        public DateTime? CTIMEFrom { get; set; }

        /// <summary>
        /// 订单（包括采购、销售）创建时间过滤的“右端点”，使用闭区间，就是<=
        /// </summary>
        public DateTime? CTIMETimeTo { get; set; }

        /// <summary>
        /// （包括采购、销售）通过半角逗号“,”分割的多个货品的ProductKey
        /// </summary>
        public string ProductKeys { get; set; }

        /// <summary>
        /// 通过"国家/厂号/货品名/品牌" ProductFullName过滤
        /// 目前只支持单个
        /// </summary>
        public string ProductFullNameFilterValue { get; set; }


        /// <summary>
        /// （采购适用，销售自动忽略）ETA过滤的“左端点”，使用闭区间，就是>=
        /// </summary>
        public DateTime? ETAFrom { get; set; }

        /// <summary>
        /// （采购适用，销售自动忽略）ETA过滤的“右端点”，使用闭区间，就是<=
        /// </summary>
        public DateTime? ETATo { get; set; }

        /// <summary>
        /// （采购适用，销售自动忽略）供应商过滤，空值则是不过滤
        /// </summary>
        public int? SupplierId { get; set; }


        /// <summary>
        /// （销售适用，采购自动忽略）单据号或供应商
        /// </summary>
        public string SerialOrSupplierFilterValue
        {
            get;
            set;
        }

        /// <summary>
        /// 默认条件：
        /// IsEnable = true, Page = 1, Rows = 10
        /// ListInclude = ContractListInclude.BothOrderAndSaleContract,
        /// </summary>
        /// <returns></returns>
        internal static ContractListCondition GetDefault()
        {
            return new ContractListCondition()
            {
                IsEnable = true,
                ListInclude = ContractListInclude.BothOrderAndSaleContract,
                Page = 1,
                Rows = 10
            };
        }

        public ContractOrderField OrderField
        {
            get;
            set;
        }
    }
}
