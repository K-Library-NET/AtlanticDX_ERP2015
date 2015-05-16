using AtlanticDX.Model.Areas.Sales.Models;
using PrivilegeFramework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using YuShang.ERP.Entities.ResMgr;

namespace AtlanticDX.Model.Helper
{
    public class OrdersHelper
    {
        #region ordercontract

        //FIXED  供应商列表
        public static IEnumerable<SelectListItem> GetSuppliers()
        {
            IEnumerable<Supplier> suppliers = ResBusinessManager.Instance.GetSuppliers();

            if (suppliers != null && suppliers.Count() > 0)
            {
                var temp = from one in suppliers
                           select new SelectListItem()
                           {
                               Text = one.SupplierName,
                               Value = one.SupplierId.ToString()
                           };

                return temp.ToList();
            }

            return new SelectListItem[] { };
        }

        //FIXED  港口代理列表（应该使用报关公司）
        public static IEnumerable<SelectListItem> GetHarborAgents()
        {
            IEnumerable<DeclarationCompany> harborAgents =
                ResBusinessManager.Instance.GetDeclarationCompanies();

            if (harborAgents != null && harborAgents.Count() > 0)
            {
                var temp = from one in harborAgents
                           select new SelectListItem()
                           {
                               Text = one.CompanyName,
                               Value = one.DeclarationCompanyId.ToString()
                           };

                return temp.ToList();
            }

            return new SelectListItem[] { };
        }

        //FIXED  香港物流公司列表
        public static IEnumerable<SelectListItem> GetHongkongLogisticsCompanies()
        {
            IEnumerable<HongkongLogisticsCompany> companies =
                ResBusinessManager.Instance.GetHKLogisticsCompanies();

            if (companies != null && companies.Count() > 0)
            {
                var temp = from one in companies
                           select new SelectListItem()
                           {
                               Text = one.CompanyName,
                               Value = one.HongkongLogisticsCompanyId.ToString()
                           };

                return temp.ToList();
            }

            return new SelectListItem[] { };
        }

        /// <summary>
        /// 内地物流公司列表
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> GetMainlandLogisticsCompanies()
        {
            IEnumerable<MainlandLogisticsCompany> companies =
                ResBusinessManager.Instance.GetMainlandLogisticsCompanies();

            if (companies != null && companies.Count() > 0)
            {
                var temp = from one in companies
                           select new SelectListItem()
                           {
                               Text = one.CompanyName,
                               Value = one.MainlandLogisticsCompanyId.ToString()
                           };

                return temp.ToList();
            }

            return new SelectListItem[] { };
        }

        /// <summary>
        /// 目的地：（港口列表）
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> GetHarbors()
        {
            IEnumerable<Harbor> harbors = ResBusinessManager.Instance.GetHarbors();

            if (harbors != null && harbors.Count() > 0)
            {
                var temp = from one in harbors
                           select new SelectListItem()
                           {
                               Text = one.HarborName,
                               Value = one.HarborId.ToString()
                           };

                return temp.ToList();
            }

            return new SelectListItem[] { };
        }

        #endregion

        #region salecontract

        public static IEnumerable<SelectListItem> GetSaleClients()
        {
            IEnumerable<SaleClient> clients = ResBusinessManager.Instance.GetSaleClients();

            if (clients != null && clients.Count() > 0)
            {
                var temp = from one in clients
                           select new SelectListItem()
                           {
                               Text = one.CompanyName,
                               Value = one.SaleClientId.ToString()
                           };

                return temp.ToList();
            }

            return new SelectListItem[] { };
        }

        /// <summary>
        /// 根据SaleProductItemId获取还价
        /// </summary>
        /// <param name="saleProductItemId"></param>
        /// <returns></returns>
        public static SaleProductItemBargainsViewModel GetBargainsItemBySaleProductItemId(
            int saleProductItemId)
        {
            using (AtlanticDXContext context = new AtlanticDXContext())
            {
                var salebargainItems = context.SaleBargainItems.Where(
                    m => m.SaleProductItemId == saleProductItemId);
                if (salebargainItems != null && salebargainItems.Count() > 0)
                {
                    var resultItem = from one in salebargainItems
                                     select new SaleProductItemBargainItemViewModel(one);

                    return new SaleProductItemBargainsViewModel(resultItem);
                }
            }

            return null;
        }

        /// <summary>
        /// 传入销售人员ID、销售合同ID，数据库操作Context
        /// 返回创建或更新一个选择还价
        /// </summary>
        /// <param name="saleContractId"></param>
        /// <param name="salesmanId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static YuShang.ERP.Entities.Sale.SaleBargain
            SelectAndCreateSaleBargain(int saleContractId, int salesmanId,
            AtlanticDXContext db)
        {
            var contract = db.SaleContracts.Find(saleContractId);
            if (contract != null)
            {
                YuShang.ERP.Entities.Sale.SaleBargain bargain = db.SaleBargains.Create();
                if (contract.SelectedSaleBargain != null)
                {
                    bargain = contract.SelectedSaleBargain;
                }
                else
                {
                    db.SaleBargains.Add(bargain);
                }

                var items = db.SaleBargainItems.Where(item => item.SalesmanId == salesmanId
                     && item.SaleProductItem.SaleContractId == saleContractId);
                bargain.BargainItems = items.ToList();
                bargain.BargainSalesmanId = salesmanId;
                bargain.SaleContractId = saleContractId;
                contract.SelectedSaleBargain = bargain;
                return bargain;
            }

            return null;
        }

        #endregion
    }
}