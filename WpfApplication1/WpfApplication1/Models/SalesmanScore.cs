using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Models
{
    [Table("Tb_Sale_SalesmanScore")]
    public class SalesmanScore
    {
        public int SalesmanScoreId
        {
            get;
            set;
        }

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

        public float ScoreValue
        {
            get;
            set;
        }

        public string Comments
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
