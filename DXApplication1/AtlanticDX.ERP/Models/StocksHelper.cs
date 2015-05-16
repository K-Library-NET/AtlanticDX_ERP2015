using PrivilegeFramework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using YuShang.ERP.Entities.ResMgr;

namespace AtlanticDX.Model.Helper
{
    public class StocksHelper
    {
        public static IEnumerable<SelectListItem> GetStoreHouses()
        {
            IEnumerable<StoreHouse> storeHouses =
                ResBusinessManager.Instance.GetStoreHouses();

            if (storeHouses != null && storeHouses.Count() > 0)
            {
                var temp = from one in storeHouses
                           select new SelectListItem()
                           {
                               Text = one.StoreHouseName,
                               Value = one.StoreHouseId.ToString()
                           };

                return temp.ToList();
            }

            return new SelectListItem[] { };
        }
    }
}