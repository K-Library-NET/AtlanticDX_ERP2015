namespace AtlanticDX.Model.Areas.Orders.Models
{
    public class AddSalesGuidePriceViewModel
    {
     
        public int ProductItemId
        {
            get;
            set;
        }

        /// <summary>
        /// 指导销售价
        /// </summary>
        public double SalesGuidePrice
        {
            get;
            set;
        }
    }
}