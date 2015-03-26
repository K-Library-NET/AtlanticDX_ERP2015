using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Finances
{
    /// <summary>
    /// 财务记录与业务对象（财务对象、业务实体对象）的关联对象
    /// </summary>
    public class AccountsRecordRelation
    {
        /// <summary>
        /// 财务记录Id
        /// </summary>
        [Key]
        [Column(Order = 1)]
        public int AccountsRecordId
        {
            get;
            set;
        }

        /// <summary>
        /// 财务记录Id
        /// Navigation Property
        /// </summary>
        public virtual AccountsRecord AccountsRecord
        {
            get;
            set;
        }

        /// <summary>
        /// 业务对象ID
        /// </summary>
        [Key]
        [Column(Order = 2)]
        public int RelatedObjectId
        {
            get;
            set;
        }

        /// <summary>
        /// 实收或实付账款对象Id与业务对象的关联类型
        /// </summary>
        [Display(Name = "关联类型")]
        [Required]
        public FinancialRelatedObjectType RelatedObjectType
        {
            get;
            set;
        }
    }
}
