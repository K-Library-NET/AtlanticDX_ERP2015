using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Models
{
    [Table("Tb_Sale_Book")]
    public class SaleBook
    {
        public int SaleBookId
        {
            get;
            set;
        }

        public int SalesmanId
        {
            get;
            set;
        }


        public string WXCode
        {
            get;
            set;
        }

        public string WXNickname
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }

        public string Comments
        {
            get;
            set;
        }

        /// <summary>
        /// 默认为0，1为现场确认
        /// </summary>
        public int ViewConfirm
        {
            get;
            set;
        }

        /// <summary>
        /// 默认为0，1为签约买楼意向后的确认
        /// </summary>
        public int BuyConfirm
        {
            get;
            set;
        }

        /// <summary>
        /// 意向单元
        /// </summary>
        public string BuyUnit
        {
            get;
            set;
        }

        public string Column1
        {
            get;
            set;
        }

        public string Column2
        {
            get;
            set;
        }

        public string Column3
        {
            get;
            set;
        }

        public int CompanyId
        {
            get;
            set;
        }
    }
}
