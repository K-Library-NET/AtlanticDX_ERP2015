﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YuShang.ERP.Entities.ResMgr;
using YuShang.ERP.Entities.Stocks;

namespace AtlanticDX.ERP.Areas.Stocks.Models
{
    /// <summary>
    /// 按照仓库来组织库存显示
    /// </summary>
    public class StockHouseViewModel
    {
        public int StockHouseViewModelId
        {
            get
            {
                return this.StoreHouse.StoreHouseId;
            }
            set
            {
                this.StoreHouse.StoreHouseId = value;
            }
        }

        public StockHouseViewModel(StoreHouse storeHouse)
        {
            this.StoreHouse = storeHouse;
        }

        /// <summary>
        /// 仓库
        /// </summary>
        public StoreHouse StoreHouse { get; set; }

        /// <summary>
        /// 仓库对应的库存
        /// </summary>
        public IEnumerable<StockItemViewModel> StockItems { get; set; }
    }
}