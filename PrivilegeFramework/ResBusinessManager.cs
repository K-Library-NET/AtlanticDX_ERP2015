using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.ResMgr;

namespace PrivilegeFramework
{
    public class ResBusinessManager
    {
        protected ResBusinessManager()
        {

        }

        private static ResBusinessManager m_manager = null;

        public static ResBusinessManager Instance
        {
            get
            {
                if (m_manager == null)
                    m_manager = new ResBusinessManager();

                return m_manager;
            }
        }

        public void ClearCache(string key)
        {
            try
            {
                object temp = null;
                if (!string.IsNullOrEmpty(key) && m_cacheMap.ContainsKey(key))
                    m_cacheMap.TryRemove(key, out temp);
            }
            catch (Exception eee)
            {
                UtilityFramework.LogHelper.Error("ResBusinessManager.ClearCache", eee);
            }
        }

        private System.Collections.Concurrent.ConcurrentDictionary<string, object>
            m_cacheMap = new System.Collections.Concurrent.ConcurrentDictionary<string, object>();

        public const string RESOURCES_SUPPLIERS = "ResMgr.Suppliers";
        public const string RESOURCES_DECLARATION_COMPANIES = "ResMgr.DeclarationCompany";
        public const string RESOURCES_HKLOGIS_COMPANIES = "ResMgr.HongkongLogisticsCompany";

        public const string RESOURCES_HARBORS = "ResMgr.Harbor";
        public const string RESOURCES_MAINLANDLOGIS_COMPANIES = "ResMgr.MainlandLogisticsCompany";
        public const string RESOURCES_PRODUCTS = "ResMgr.Product";
        public const string RESOURCES_SALECLIENTS = "ResMgr.SaleClient";
        public const string RESOURCES_STOREHOUSES = "ResMgr.StoreHouse";

        public const string RESOURCES_SALE_CLIENTS = "ResMgr.SaleClient";

        public IEnumerable<YuShang.ERP.Entities.ResMgr.Supplier> GetSuppliers()
        {
            if (m_cacheMap.ContainsKey(RESOURCES_SUPPLIERS))
            {
                object obj = null;
                bool exists = m_cacheMap.TryGetValue(RESOURCES_SUPPLIERS, out obj);
                if (exists && obj != null && obj is IEnumerable<YuShang.ERP.Entities.ResMgr.Supplier>)
                    return obj as IEnumerable<YuShang.ERP.Entities.ResMgr.Supplier>;
            }

            IEnumerable<YuShang.ERP.Entities.ResMgr.Supplier> suppliers
                = this.GetSuppliersCore();

            TryUpdateConcurrent(RESOURCES_SUPPLIERS, suppliers);

            //if (m_cacheMap.ContainsKey(RESOURCES_SUPPLIERS))
            //    m_cacheMap.add[RESOURCES_SUPPLIERS] = suppliers;
            //else
            //    m_cacheMap.Add(RESOURCES_SUPPLIERS, suppliers);

            return suppliers;
        }

        private void TryUpdateConcurrent(string key, object source)
        {
            object temp = null;
            m_cacheMap.TryRemove(key, out temp);
            bool succeed = m_cacheMap.TryAdd(key, source);
            if (succeed == false)
            {
                UtilityFramework.LogHelper.Debug("ResBusinessManager TryAdd failed.");
            }
        }

        private IEnumerable<YuShang.ERP.Entities.ResMgr.Supplier> GetSuppliersCore()
        {
            using (ExtendedIdentityDbContext dxContext = ExtendedIdentityDbContext.Create())
            {
                return dxContext.Suppliers.AsNoTracking().AsParallel()
                    .Where(m => m.IsDeleted == false)
                    .OrderBy(m => m.SupplierName).ToList();
            }
        }

        public IEnumerable<DeclarationCompany> GetDeclarationCompanies()
        {
            if (m_cacheMap.ContainsKey(RESOURCES_DECLARATION_COMPANIES))
            {
                object obj = null;
                bool exists = m_cacheMap.TryGetValue(RESOURCES_DECLARATION_COMPANIES, out obj);
                if (exists && obj != null && obj is IEnumerable<YuShang.ERP.Entities.ResMgr.DeclarationCompany>)
                    return obj as IEnumerable<YuShang.ERP.Entities.ResMgr.DeclarationCompany>;
            }

            IEnumerable<YuShang.ERP.Entities.ResMgr.DeclarationCompany> coms
                = this.GetDeclarationCompaniesCore();

            this.TryUpdateConcurrent(RESOURCES_DECLARATION_COMPANIES, coms);

            //if (m_cacheMap.ContainsKey(RESOURCES_DECLARATION_COMPANIES))
            //    m_cacheMap[RESOURCES_DECLARATION_COMPANIES] = coms;
            //else
            //    m_cacheMap.Add(RESOURCES_DECLARATION_COMPANIES, coms);

            return coms;
        }

        private IEnumerable<DeclarationCompany> GetDeclarationCompaniesCore()
        {
            using (ExtendedIdentityDbContext dxContext = ExtendedIdentityDbContext.Create())
            {
                return dxContext.DeclarationCompanies.AsNoTracking().AsParallel()
                    .Where(m => m.IsDeleted == false || m.DeclarationCompanyId == 0)
                    .OrderBy(m => m.DeclarationCompanyId).ThenBy(m => m.CompanyName).ToList();
            }
        }

        public IEnumerable<HongkongLogisticsCompany> GetHKLogisticsCompanies()
        {
            if (m_cacheMap.ContainsKey(RESOURCES_HKLOGIS_COMPANIES))
            {
                object obj = null;
                bool exists = m_cacheMap.TryGetValue(RESOURCES_HKLOGIS_COMPANIES, out obj);
                if (exists && obj != null && obj is IEnumerable<YuShang.ERP.Entities.ResMgr.HongkongLogisticsCompany>)
                    return obj as IEnumerable<YuShang.ERP.Entities.ResMgr.HongkongLogisticsCompany>;
            }

            IEnumerable<YuShang.ERP.Entities.ResMgr.HongkongLogisticsCompany> coms
                = this.GetHKLogisticsCompaniesCore();

            this.TryUpdateConcurrent(RESOURCES_HKLOGIS_COMPANIES, coms);

            //if (m_cacheMap.ContainsKey(RESOURCES_HKLOGIS_COMPANIES))
            //    m_cacheMap[RESOURCES_HKLOGIS_COMPANIES] = coms;
            //else
            //    m_cacheMap.Add(RESOURCES_HKLOGIS_COMPANIES, coms);

            return coms;
        }

        private IEnumerable<HongkongLogisticsCompany> GetHKLogisticsCompaniesCore()
        {
            using (ExtendedIdentityDbContext dxContext = ExtendedIdentityDbContext.Create())
            {
                return dxContext.HKLogistics.AsNoTracking().AsParallel()
                    .Where(m => m.IsDeleted == false || m.HongkongLogisticsCompanyId == 0)
                    .OrderBy(m => m.HongkongLogisticsCompanyId).ThenBy(m => m.CompanyName).ToList();
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            if (m_cacheMap.ContainsKey(RESOURCES_PRODUCTS))
            {
                object obj = null;
                bool exists = m_cacheMap.TryGetValue(RESOURCES_PRODUCTS, out obj);
                if (exists && obj != null && obj is IEnumerable<YuShang.ERP.Entities.ResMgr.Product>)
                    return obj as IEnumerable<YuShang.ERP.Entities.ResMgr.Product>;
            }

            IEnumerable<YuShang.ERP.Entities.ResMgr.Product> coms
                = this.GetProductsCore();

            this.TryUpdateConcurrent(RESOURCES_PRODUCTS, coms);

            return coms;
        }

        private IEnumerable<Product> GetProductsCore()
        {
            using (ExtendedIdentityDbContext dxContext = ExtendedIdentityDbContext.Create())
            {
                return dxContext.Products.AsNoTracking().AsParallel()
                    .Where(m => m.IsDeleted == false)
                    .OrderBy(m => m.ProductName).ToList();
            }
        }

        public IEnumerable<Harbor> GetHarbors()
        {
            if (m_cacheMap.ContainsKey(RESOURCES_HARBORS))
            {
                object obj = null;
                bool exists = m_cacheMap.TryGetValue(RESOURCES_HARBORS, out obj);
                if (exists && obj != null && obj is IEnumerable<YuShang.ERP.Entities.ResMgr.Harbor>)
                    return obj as IEnumerable<YuShang.ERP.Entities.ResMgr.Harbor>;
            }

            IEnumerable<YuShang.ERP.Entities.ResMgr.Harbor> coms
                = this.GetHarborsCore();

            this.TryUpdateConcurrent(RESOURCES_HARBORS, coms);

            return coms;
        }

        private IEnumerable<Harbor> GetHarborsCore()
        {
            using (ExtendedIdentityDbContext dxContext = ExtendedIdentityDbContext.Create())
            {
                return dxContext.Harbors.AsNoTracking().AsParallel()
                    .Where(m => m.IsDeleted == false)
                    .OrderBy(m => m.HarborNameENG).ToList();
            }
        }

        public IEnumerable<MainlandLogisticsCompany> GetMainlandLogisticsCompanies()
        {
            if (m_cacheMap.ContainsKey(RESOURCES_MAINLANDLOGIS_COMPANIES))
            {
                object obj = null;
                bool exists = m_cacheMap.TryGetValue(RESOURCES_MAINLANDLOGIS_COMPANIES, out obj);
                if (exists && obj != null && obj is IEnumerable<YuShang.ERP.Entities.ResMgr.MainlandLogisticsCompany>)
                    return obj as IEnumerable<YuShang.ERP.Entities.ResMgr.MainlandLogisticsCompany>;
            }

            IEnumerable<YuShang.ERP.Entities.ResMgr.MainlandLogisticsCompany> coms
                = this.GetMainlandLogisticsCompaniesCore();

            this.TryUpdateConcurrent(RESOURCES_MAINLANDLOGIS_COMPANIES, coms);

            return coms;
        }

        private IEnumerable<MainlandLogisticsCompany> GetMainlandLogisticsCompaniesCore()
        {
            using (ExtendedIdentityDbContext dxContext = ExtendedIdentityDbContext.Create())
            {
                return dxContext.MainlandLogistics.AsNoTracking().AsParallel()
                    .Where(m => m.IsDeleted == false || m.MainlandLogisticsCompanyId == 0)
                    .OrderBy(m => m.MainlandLogisticsCompanyId).ThenBy(m => m.CompanyName).ToList();
            }
        }

        public IEnumerable<SaleClient> GetSaleClients()
        {
            if (m_cacheMap.ContainsKey(RESOURCES_SALE_CLIENTS))
            {
                object obj = null;
                bool exists = m_cacheMap.TryGetValue(RESOURCES_SALE_CLIENTS, out obj);
                if (exists && obj != null && obj is IEnumerable<YuShang.ERP.Entities.ResMgr.SaleClient>)
                    return obj as IEnumerable<YuShang.ERP.Entities.ResMgr.SaleClient>;
            }

            IEnumerable<YuShang.ERP.Entities.ResMgr.SaleClient> coms
                = this.GetSaleClientsCore();

            this.TryUpdateConcurrent(RESOURCES_SALE_CLIENTS, coms);

            return coms;
        }

        private IEnumerable<SaleClient> GetSaleClientsCore()
        {
            using (ExtendedIdentityDbContext dxContext = ExtendedIdentityDbContext.Create())
            {
                return dxContext.SaleClients.AsNoTracking().AsParallel()
                    .Where(m => m.IsDeleted == false)
                    .OrderBy(m => m.CompanyName).ToList();
            }
        }

        public IEnumerable<StoreHouse> GetStoreHouses()
        {
            if (m_cacheMap.ContainsKey(RESOURCES_STOREHOUSES))
            {
                object obj = null;
                bool exists = m_cacheMap.TryGetValue(RESOURCES_STOREHOUSES, out obj);
                if (exists && obj != null && obj is IEnumerable<StoreHouse>)
                    return obj as IEnumerable<StoreHouse>;
            }

            IEnumerable<StoreHouse> coms = this.GetStoreHousesCore();

            this.TryUpdateConcurrent(RESOURCES_STOREHOUSES, coms);

            return coms;
        }

        private IEnumerable<StoreHouse> GetStoreHousesCore()
        {
            using (ExtendedIdentityDbContext dxContext = ExtendedIdentityDbContext.Create())
            {
                return dxContext.StoreHouses.AsNoTracking().AsParallel()
                    .Where(m => m.IsDeleted == false)
                    .OrderBy(m => m.StoreHouseName).ToList();
            }
        }
    }
}
