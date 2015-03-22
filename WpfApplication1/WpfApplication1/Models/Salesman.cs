using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Models
{
    [Table("Tb_Sale_Salesman")]
    public class Salesman
    {
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

        public string Company
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public string Comments
        {
            get;
            set;
        }

        public DateTime CreateDate
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

        public int CompanyID
        {
            get;
            set;
        }
    }
}
