
/*easyui 时间列formatter*/
function easyui_time_formatter(value, row, index) {
    if (/^\/Date\([0-9]+\)\/$/.test(value)) {
        var date = eval('new ' + eval(value).source);
        return date.toLocaleString();
    } else {
        return '';
    }
}

/*easyui 订单状态列formatter*/
function func_contractStatus_formatter(value, row, index) {
    var temp = '';
    if (value == 0) {
        temp = '未审核';
    } else if (value == 1) {
        temp = '审核通过';
    } else if (value == 2) {
        temp = '审核不通过';
    } else if (value == 3) {
        temp = '已结单';
    }
    return temp;
}

/*easyui 订单类型列formatter*/
function func_ordertype_formatter(value, row, index) {
    var temp = '';
    if (row.ContractType != undefined && row.ContractType == 0) return '';
    if (value == 0) {
        temp = '期货';
    } else if (value == 1) {
        temp = '现货';
    }
    return temp;
}

function easyui_field_formatter(value, row, index) {
    return eval("row." + this.field);
}


/*产品模板行*/
var productDetailFormatter = function (rowIndex, rowData) {
    var temp = '<table border="0" cellspacing="1" cellpadding="0" style="background-color:#ccc;" class="the_xlb">';
    temp += '<tr><td colspan="10" style="border:none; text-align:left; font-size:16px;">' + '商品信息'
        + '</td><td colspan="5" style="border:none; font-size:16px; text-align:center;">' + '香港物流信息'
        + '</td><td colspan="5" style="border:none; font-size:16px; text-align:center;">' + '内地物流信息' + '</td></tr>';
    temp += '<tr><td style="border:none; text-align:center; font-size:14px; width:150px;">' + '货品出厂编号'
        + '</td><td style="border:none; text-align:center; font-size:14px; width:120px;">' + '货品名'
        + '</td><td style="border:none; text-align:center; font-size:14px; width:120px;">' + '国家'
        + '</td><td style="border:none; text-align:center; font-size:14px; width:120px;">' + '厂号'
        + '</td><td style="border:none; text-align:center; font-size:14px; width:120px;">' + '品牌'
        + '</td><td style="border:none; text-align:center; font-size:14px; width:100px;">' + '件数'
        + '</td><td style="border:none; text-align:center; font-size:14px; width:100px;">' + '净重'
        + '</td><td style="border:none; text-align:center; font-size:14px; width:100px;">' + '单价'
        + '</td><td style="border:none; text-align:center; font-size:14px; width:120px;">' + '采购币种'
        + '</td><td style="border:none; text-align:center; font-size:14px; width:150px;">' + '货款小计'
        + '</td><td style="border:none; text-align:center; font-size:14px; width:80px;">' + '收单件数'
        + '</td><td style="border:none; text-align:center; font-size:14px; width:80px;">' + '收单吨重'
        + '</td><td style="border:none; text-align:center; font-size:14px; width:80px;">' + '运费/吨'
        + '</td><td style="border:none; text-align:center; font-size:14px; width:80px;">' + '保险'
        + '</td><td style="border:none; text-align:center; font-size:14px; width:80px;">' + '运费小计'
        + '</td><td style="border:none; text-align:center; font-size:14px; width:80px;">' + '收单件数'
        + '</td><td style="border:none; text-align:center; font-size:14px; width:80px;">' + '收单吨重'
        + '</td><td style="border:none; text-align:center; font-size:14px; width:80px;">' + '运费/吨'
        + '</td><td style="border:none; text-align:center; font-size:14px; width:80px;">' + '保险'
        + '</td><td style="border:none; text-align:center; font-size:14px; width:80px;">' + '运费小计'
        + '</td></tr>';
    //liangdawen 20150205 
    var rows = rowData.ContractType == 0 ? rowData.ContractItems : rowData.SaleProductItems;
    var productitemrows = rowData.ContractItems;
    //香港物流：根据for循环的i去写入
    var hklogisItems = rowData.HongkongLogistics == null || rowData.HongkongLogistics.IsEnable == false ?
        null : rowData.HongkongLogistics.HongKongLogisticsItems;
    //内地物流：根据for循环的i去写入
    var mllogisItems = rowData.MainlandLogistics == null || rowData.MainlandLogistics.IsEnable == false ?
        null : rowData.MainlandLogistics.LogisItems;

    //var rows = rowData.OrderType == 0 ? rowData.ContractItems : rowData.SaleProductItems;
    if (rows == null) return '';
    for (i = 0; i < rows.length; i++) {
        temp += '<tr>' +
        '<td style="border:0;padding-right:10px">' + rows[i].ProductKey +
        '</td>' +
        '<td style="border:0;padding-right:10px">' + rows[i].ProductName +
        '</td>' +
        '<td style="border:0;padding-right:10px">' + (productitemrows[i] != null && productitemrows[i].Product != null && productitemrows[i].Product.MadeInCountry != null ? productitemrows[i].Product.MadeInCountry : '') +
        '</td>' +
        '<td style="border:0;padding-right:10px">' + (productitemrows[i] != null && productitemrows[i].Product != null && productitemrows[i].Product.MadeInFactory != null ? productitemrows[i].Product.MadeInFactory : '') +
        '</td>' +
        '<td style="border:0;padding-right:10px">' + (productitemrows[i] != null && productitemrows[i].Product != null && productitemrows[i].Product.Brand != null ? productitemrows[i].Product.Brand : '') +
        '</td>' +
        '<td style="border:0;padding-right:10px; text-align:center;">' + (productitemrows[i] != null && productitemrows[i].Quantity != null ? productitemrows[i].Quantity : '') +
        '</td>' +
        '<td style="border:0;padding-right:10px; text-align:center;">' + (productitemrows[i] != null && productitemrows[i].NetWeight != null ? productitemrows[i].NetWeight : '') +
        '</td>' +
        '<td style="border:0; text-align:center;">' + (productitemrows[i] != null && productitemrows[i].UnitPrice != null ? productitemrows[i].UnitPrice : '') +
        '</td>' +
        '<td style="border:0;padding-right:10px; text-align:center;">' + (productitemrows[i] != null && productitemrows[i].Currency != null ? productitemrows[i].Currency : '') +
        '</td>' +
         '<td style="border:0;padding-right:10px">' + rows[i].SubTotal +
        '</td>' +
         '<td style="border:0;padding-right:10px">' + (hklogisItems != null && hklogisItems[i] != null && hklogisItems[i].ContractQuantity ? hklogisItems[i].ContractQuantity : '') +
        '</td>' +
        '<td style="border:0;padding-right:10px">' + (hklogisItems != null && hklogisItems[i] != null && hklogisItems[i].ContractWeight ? hklogisItems[i].ContractWeight : '') +
        '</td>' +
        '<td style="border:0;padding-right:10px; text-align:center;">' + (hklogisItems != null && hklogisItems[i] != null && hklogisItems[i].FreightCharges ? hklogisItems[i].FreightCharges : '') +
        '</td>' +
        '<td style="border:0;padding-right:10px; text-align:center;">' + (hklogisItems != null && hklogisItems[i] != null && hklogisItems[i].Insurance ? hklogisItems[i].Insurance : '') +
        '</td>' +
        '<td style="border:0; text-align:center;">' + (hklogisItems != null && hklogisItems[i] != null && hklogisItems[i].SubTotal ? hklogisItems[i].SubTotal : '') +
        '</td>' +
         '<td style="border:0;padding-right:10px">' + (mllogisItems != null && mllogisItems[i] != null && mllogisItems[i].ContractQuantity ? mllogisItems[i].ContractQuantity : '') +
        '</td>' +
        '<td style="border:0;padding-right:10px">' + (mllogisItems != null && mllogisItems[i] != null && mllogisItems[i].ContractWeight ? mllogisItems[i].ContractWeight : '') +
        '</td>' +
        '<td style="border:0;padding-right:10px; text-align:center;">' + (mllogisItems != null && mllogisItems[i] != null && mllogisItems[i].FreightCharges ? mllogisItems[i].FreightCharges : '') +
        '</td>' +
        '<td style="border:0;padding-right:10px; text-align:center;">' + (mllogisItems != null && mllogisItems[i] != null && mllogisItems[i].Insurance ? mllogisItems[i].Insurance : '') +
        '</td>' +
        '<td style="border:0; text-align:center;">' + (mllogisItems != null && mllogisItems[i] != null && mllogisItems[i].SubTotal ? mllogisItems[i].SubTotal : '') +
        '</td>' +
        '</tr>';
    }
    temp += '</table>';
    return temp;
}


/*销售订单产品模板行*/
var saleProductDetailFormatter = function (rowIndex, rowData) {
    var temp = '<table border="0" cellspacing="1" cellpadding="0" style="background-color:#ccc;" class="the_xlb">';
    //temp += '<tr><td colspan="7" style="border:none; text-align:left; font-size:16px;">' +
    //    '商品信息' + '</td><td  style="border:none; font-size:16px; text-align:center;">' +
    //    '操作' + '</td>' +
    //    '</tr>';
    temp += '<tr><td style="border:none; text-align:center; font-size:14px; width:150px;">' +
        '货品出厂编号' + '</td><td style="border:none; text-align:center; font-size:14px; width:120px;">' +
        '货品名' + '</td><td style="border:none; text-align:center; font-size:14px; width:120px;">' +
        '国家' + '</td><td style="border:none; text-align:center; font-size:14px; width:120px;">' +
        '厂号' + '</td><td style="border:none; text-align:center; font-size:14px; width:120px;">' +
        '品牌' + '</td><td style="border:none; text-align:center; font-size:14px; width:100px;">' +
        '件数' + '</td><td style="border:none; text-align:center; font-size:14px; width:100px;">' +
        '入仓吨数' + '</td>' +
        '<td style="border:none; text-align:center; font-size:14px; width:100px;">销售数量</td>' +
        '<td style="border:none; text-align:center; font-size:14px; width:100px;">销售吨重</td>' +
        '<td style="border:none; text-align:center; font-size:14px; width:100px;">销售单价</td>' +
        '<td style="border:none; text-align:center; font-size:14px; width:100px;">货币</td>' +
        '<td style="border:none; text-align:center; font-size:14px; width:100px;">货款小计</td>' +
        '<td style="border:none; text-align:center; font-size:14px; width:100px;">指导销售价</td>' +
        '</tr>';
    var rows = rowData.SaleProducts;
    if (rows == null) return '';
    for (i = 0; i < rows.length; i++) {
        temp += '<tr>' +
        '<td style="border:0;padding-right:10px">' + rows[i].StockItem.ProductItem.Product.ProductKey +
        '</td>' +
        '<td style="border:0;padding-right:10px">' + rows[i].StockItem.ProductItem.Product.ProductFullName +
        '</td>' +
        '<td style="border:0;padding-right:10px">' + rows[i].StockItem.ProductItem.Product.MadeInCountry +
        '</td>' +
        '<td style="border:0;padding-right:10px">' + rows[i].StockItem.ProductItem.Product.MadeInFactory +
        '</td>' +
        '<td style="border:0;padding-right:10px">' + rows[i].StockItem.ProductItem.Product.Brand +
        '</td>' +
        '<td style="border:0;padding-right:10px; text-align:center;">' + rows[i].StockItem.Quantity +
        '</td>' +
        '<td style="border:0;padding-right:10px; text-align:center;">' + rows[i].StockItem.StockWeight +
        '</td>' +
        '<td style="border:0;padding-right:10px; text-align:center;">' + rows[i].Quantity + '</td>' +
        '<td style="border:0;padding-right:10px; text-align:center;">' + rows[i].Weight + '</td>' +
        '<td style="border:0;padding-right:10px; text-align:center;">' + rows[i].UnitPrice + '</td>' +
        '<td style="border:0;padding-right:10px; text-align:center;">' + rows[i].Currency + '</td>' +
        '<td style="border:0;padding-right:10px; text-align:center;">' + rows[i].SubTotal + '</td>' +
        '<td style="border:0;padding-right:10px; text-align:center;">' + rows[i].StockItem.ProductItem.SalesGuidePrice + '</td>' +
        '</tr>';
    }
    temp += '</table>';
    return temp;
}


function getVal(objStr) {
    return objStr == null || objStr == undefined ? '' : objStr;
}

function func_operation_formatter(value, row, index) {
    var temp = '';
    if (row.ContractStatus == 0) {
        temp = '<a href="javascript:;" class="edit" onclick="edit_order_contract(' + index + ')">编辑</a>';
        if (row.ContractType == 1) {
            temp += '<a href="javascript:;" class="audit" onclick="audit_sale_contract(\'' + index + '\')">审核</a>';
        }
    }
    if (row.ContractType == 1) {
        temp += '<a href="javascript:;" class="bargain" onclick="add_sale_contract_bargain(\'' + index + '\')">还价</a>';
    }
    return temp;
}

//交单操作
function func_os_operation_formatter(value, row, index) {
    var temp = '';
    if (row.ContractStatus == 0) {
        temp = '<a href="javascript:;" class="audit" onclick="audit_row(' + index + ')">审核</a>';
    }
    return temp;
}


//交单列表数据加载完成后
function datagrid_onLoadSuccess(data) {
    $('span.sum_mec').eq(0).text('数量小计:' + data.ProductsTotal);
    $('span.sum_mec').eq(1).text('采购额小计:' + data.PaymentTotal);
}

function func_ContractType_formatter(value, row, index) {
    var temp = '';
    if (row.ContractType == 0) {
        temp = '采购订单';
    } else if (row.ContractType == 1) {
        temp = '销售订单';
    }
    return temp;
}

function edit_order_contract(index) {
    var row = get_datagrid_row_by_index(index);
    if (row.ContractType == 0) {
        parent.addMainTab('编辑采购合同', '/Orders/OrderContract/Edit?OrderContractKey=' + row.ContractKey, true, true);
    } else if (row.ContractType == 1) {
        if (row.OrderType == 0) {
            parent.addMainTab('编辑期货销售合同', '/Sales/SalesByFutures/Edit?SaleContractKey=' + row.ContractKey,true,true);
        } else if (row.OrderType == 1) {
            parent.addMainTab('编辑现货销售合同', '/Sales/SalesByInventories/Edit?SaleContractKey=' + row.ContractKey,true,true);
        }
    }
}

function audit_sale_contract(index) {
    var row = get_datagrid_row_by_index(index);
    var controller = row.OrderType == 0 ? 'SalesByFutures' : 'SalesByInventories';
    var tapName = row.OrderType == 0 ? '审核期货销售' : '审核现货销售';
    parent.addMainTab(tapName, '/Sales/' + controller + '/Audit?SaleContractKey=' + row.ContractKey,true,true);
}

function add_sale_contract_bargain(index) {
    var row = get_datagrid_row_by_index(index);
    var controller = row.OrderType == 0 ? 'SalesByFutures' : 'SalesByInventories';
    parent.addMainTab('还价', '/Sales/' + controller + '/AddSaleBargin?SaleContractKey=' + row.ContractKey, true, true);
}


/*采购产品模板行*/
var productDetailFormatterNew = function (rowIndex, rowData) {
    return orderDataFormater.formatOrder(rowIndex, rowData);
}
/*采购产品交单模板行*/
var orderSubmitProductDetailFormatterNew = function (rowIndex, rowData) {
    orderDataFormater.isSubmit = true;
    return orderDataFormater.formatOrder(rowIndex, rowData);
}



//采购模版
var orderDataFormater = {
    //是否交单类型
    isSubmit: false,
    formatOrder: function (rowIndex, rowData) {
        var arrayHtml = new Array();
        if (this.isSubmit) {
            arrayHtml.push('<form onsubmit="return submit_order_submit_product_detail_form(this)">');
        }
        arrayHtml.push('<table class="mobanhang" border="0" cellspacing="1" cellpadding="0">');
        arrayHtml.push('<tr class="the_title">');
        arrayHtml.push('  <td>订单信息</td>');
        arrayHtml.push('</tr>');
        arrayHtml.push('<tr>');
        arrayHtml.push('<td style="padding:0">');
        arrayHtml.push('<table border="0" cellspacing="1" cellpadding="0" style="background-color:#666; width:100%;">');
        arrayHtml.push('  <tr>');
        arrayHtml.push('	<td class="the_left">合同编号</td>');
        arrayHtml.push('	<td>' + (this.isSubmit ? rowData.OrderContractKey : rowData.ContractKey) + '</td>');
        arrayHtml.push('	<td class="the_left">货品类型</td>');
        arrayHtml.push('	<td>' + func_ContractType_formatter(rowData.OrderType, rowData, rowIndex) + '</td>');
        arrayHtml.push('	<td class="the_left">合同类型</td>');
        arrayHtml.push('	<td>采购合同</td>');
        arrayHtml.push('	<td class="the_left">订单状态</td>');
        arrayHtml.push('	<td>' + func_contractStatus_formatter(rowData.ContractStatus, rowData, rowIndex) + '</td>');
        arrayHtml.push('  </tr>');
        arrayHtml.push('  <tr>');
        arrayHtml.push('	<td class="the_left">订单日期</td>');
        arrayHtml.push('	<td>' + rowData.CTIME_STR + '</td>');
        arrayHtml.push('	<td class="the_left">ETA</td>');
        arrayHtml.push('	<td>' + getVal(rowData.ETA) + '</td>');
        arrayHtml.push('	<td class="the_left">ETD</td>');
        arrayHtml.push('	<td>' + getVal(rowData.ETD) + '</td>');
        arrayHtml.push('	<td class="the_left">操作人</td>');
        arrayHtml.push('	<td>' + getVal(rowData.OperatorPersonName) + '</td>');
        arrayHtml.push('  </tr>');
        arrayHtml.push('  <tr>');
        arrayHtml.push('	<td class="the_left">订单船期</td>');
        arrayHtml.push('	<td>' + getVal(rowData.ShipmentPeriod) + '</td>');
        arrayHtml.push('	<td class="the_left">目的港</td>');
        arrayHtml.push('	<td>' + getVal(rowData.DestinationHarborKey) + '</td>');
        arrayHtml.push('	<td class="the_left">提货单号</td>');
        arrayHtml.push('	<td>' + getVal(rowData.DeliveryBillSerial) + '</td>');
        arrayHtml.push('	<td class="the_left">柜号</td>');
        arrayHtml.push('	<td>' + getVal(rowData.ContainerSerial) + '</td>');
        arrayHtml.push('  </tr>');
        arrayHtml.push('  <tr>');
        arrayHtml.push('	<td class="the_left">付款方式</td>');
        arrayHtml.push('	<td>' + getVal(rowData.Payment) + '</td>');
        arrayHtml.push('	<td class="the_left">货款总计</td>');
        arrayHtml.push('	<td>' + getVal(rowData.ShipmentPeriod) + '</td>');
        arrayHtml.push('	<td class="the_left">采购订金</td>');
        arrayHtml.push('	<td>' + getVal(rowData.ImportDeposite) + '</td>');
        arrayHtml.push('	<td class="the_left">采购尾款</td>');
        arrayHtml.push('	<td>' + getVal(rowData.ImportBalancedPayment) + '</td>');
        arrayHtml.push('  </tr>');
        arrayHtml.push('  <tr>');
        arrayHtml.push('	<td class="the_left">汇率</td>');
        arrayHtml.push('	<td>6.25</td>');
        arrayHtml.push('	<td class="the_left">实收金额</td>');
        arrayHtml.push('	<td>' + getVal(rowData.TotalAfterDiscount) + '</td>');
        arrayHtml.push('	<td class="the_left"></td>');
        arrayHtml.push('	<td></td>');
        arrayHtml.push('	<td class="the_left"></td>');
        arrayHtml.push('	<td></td>');
        arrayHtml.push('  </tr>');
        arrayHtml.push('</table>');
        arrayHtml.push('</td>');
        arrayHtml.push('</tr>');
        arrayHtml.push('<tr class="the_title">');
        arrayHtml.push('<td>商品信息');
        //交单模板
        if (this.isSubmit) {
            arrayHtml.push(' <input  type="submit" value="更新销售指导价" class="zdxsj" id="updateSale' + rowData.ContractKey + '">');
        }
        arrayHtml.push(' </td>');
        arrayHtml.push('</tr>');

        var rows = rowData.ContractType == 0 ? rowData.ContractItems : rowData.SaleProductItems;
        if (this.isSubmit) {
            rows = rowData.OrderProducts;
        }
        if (rows != null && rows != undefined) {
            var productitemrows = rows;
            //香港物流：根据for循环的i去写入
            var hklogisItems = rowData.HongkongLogistics == null || rowData.HongkongLogistics.IsEnable == false ? null : rowData.HongkongLogistics.LogisItems;
            if (this.isSubmit) {
                hklogisItems = rowData.HongKongLogistics == null ? null : rowData.HongKongLogistics.HongKongLogisticsItems;
            }
            hklogisItems = (hklogisItems == null || hklogisItems == undefined) ? new Array(rows.length) : hklogisItems;
            //内地物流：根据for循环的i去写入
            var mllogisItems = rowData.MainlandLogistics == null || rowData.MainlandLogistics.IsEnable == false ? null : rowData.MainlandLogistics.LogisItems;
            if (this.isSubmit) {
                mllogisItems = rowData.MainlandLogistics == null ? null : rowData.MainlandLogistics.MainlandLogisticsItems;
            }
            mllogisItems = (mllogisItems == null || mllogisItems == undefined) ? new Array(rows.length) : mllogisItems;
            //var rows = rowData.OrderType == 0 ? rowData.ContractItems : rowData.SaleProductItems;

            arrayHtml.push('<tr>');
            arrayHtml.push('<td style="padding:0">');
            arrayHtml.push('<table border="0" cellspacing="1" cellpadding="0" style="background-color:#666; width:100%;">');

            for (i = 0; i < rows.length; i++) {
                if (productitemrows[i] != null && productitemrows[i].Quantity != null) {
                    arrayHtml.push('  <tr class="the_fenge">');
                    arrayHtml.push('	<td colspan="10"></td>');
                    arrayHtml.push('  </tr>');
                    arrayHtml.push('  <tr>');
                    arrayHtml.push('	<td colspan="10" class="the_wuliu">' + rows[i].ProductName + '</td>');
                    arrayHtml.push('  </tr>');
                    arrayHtml.push('  <tr>');
                    arrayHtml.push('	<td class="the_left">货品出厂编号</td>');
                    arrayHtml.push('	<td>' + rows[i].ProductKey + '</td>');
                    arrayHtml.push('	<td class="the_left">货品名</td>');
                    arrayHtml.push('	<td>' + rows[i].ProductName + '</td>');
                    arrayHtml.push('	<td class="the_left">国家</td>');
                    arrayHtml.push('	<td>' + getVal(productitemrows[i].Product.MadeInCountry) + '</td>');
                    arrayHtml.push('	<td class="the_left">厂号</td>');
                    arrayHtml.push('	<td>' + getVal(productitemrows[i].Product.MadeInFactory) + '</td>');
                    arrayHtml.push('	<td class="the_left">品牌</td>');
                    arrayHtml.push('	<td>' + getVal(productitemrows[i].Product.Brand) + '</td>');
                    arrayHtml.push('  </tr>');
                    arrayHtml.push('  <tr>');
                    arrayHtml.push('	<td class="the_left">件数</td>');
                    arrayHtml.push('	<td>' + getVal(productitemrows[i].Quantity) + '</td>');
                    arrayHtml.push('	<td class="the_left">净重</td>');
                    arrayHtml.push('	<td>' + getVal(productitemrows[i].NetWeight) + '</td>');
                    arrayHtml.push('	<td class="the_left">单价</td>');
                    arrayHtml.push('	<td>' + getVal(productitemrows[i].UnitPrice) + '</td>');
                    arrayHtml.push('	<td class="the_left">采购币种</td>');
                    arrayHtml.push('	<td>' + getVal(productitemrows[i].Currency) + '</td>');
                    arrayHtml.push('	<td class="the_left">货款小计</td>');
                    arrayHtml.push('	<td>' + getVal(productitemrows[i].SubTotal) + '</td>');
                    arrayHtml.push('  </tr>');


                    if (this.isSubmit) {
                        arrayHtml.push('<tr>');
                        arrayHtml.push('  <td class="the_left">销售指导价</td>');
                        arrayHtml.push('  <td>');
                        arrayHtml.push('<input type="hidden" name="[' + i + '].ProductItemId" value="' + getVal(productitemrows[i].ProductItemId) + '" />');
                        arrayHtml.push('<input class="gai" style="width:49px;" type="text" name="[' + i + '].SalesGuidePrice" value="' + getVal(productitemrows[i].SalesGuidePrice) + '"></td>');
                        arrayHtml.push('  <td class="the_left"></td>');
                        arrayHtml.push('  <td></td>');
                        arrayHtml.push('  <td class="the_left"></td>');
                        arrayHtml.push('  <td></td>');
                        arrayHtml.push('  <td class="the_left"></td>');
                        arrayHtml.push('  <td></td>');
                        arrayHtml.push('  <td class="the_left"></td>');
                        arrayHtml.push('  <td></td>');
                        arrayHtml.push(' </tr>');
                    }

                    arrayHtml.push('  <tr>');
                    arrayHtml.push('	<td colspan="10" class="the_wuliu">香港物流</td>');
                    arrayHtml.push('  </tr>');
                    arrayHtml.push('  <tr>');
                    arrayHtml.push('	<td class="the_left">收单件数</td>');

                    if (hklogisItems[i] != undefined) {
                        arrayHtml.push('	<td>');
                        arrayHtml.push(getVal(hklogisItems[i].ContractQuantity));
                        arrayHtml.push('	</td><td class="the_left">收单顿重</td>');
                        arrayHtml.push('	<td>');
                        arrayHtml.push(getVal(hklogisItems[i].ContractWeight));
                        arrayHtml.push('</td>');
                        arrayHtml.push('	<td class="the_left">运费/吨</td>');
                        arrayHtml.push('	<td>' + getVal(hklogisItems[i].FreightCharges) + '</td>');
                        arrayHtml.push('	<td class="the_left">保险</td>');
                        arrayHtml.push('	<td>' + getVal(hklogisItems[i].Insurance) + '</td>');
                        arrayHtml.push('	<td class="the_left">运费小计</td>');
                        arrayHtml.push('	<td>' + getVal(hklogisItems[i].SubTotal) + '</td>');
                    }
                    else {
                        arrayHtml.push('	<td></td>');
                        arrayHtml.push('	<td class="the_left">收单顿重</td>');
                        arrayHtml.push('	<td></td>');
                        arrayHtml.push('	<td class="the_left">运费/吨</td>');
                        arrayHtml.push('	<td></td>');
                        arrayHtml.push('	<td class="the_left">保险</td>');
                        arrayHtml.push('	<td></td>');
                        arrayHtml.push('	<td class="the_left">运费小计</td>');
                        arrayHtml.push('	<td></td>');
                    }
                    arrayHtml.push('  </tr>');

                    if (mllogisItems) {
                        arrayHtml.push('  <tr>');
                        arrayHtml.push('	<td colspan="10" class="the_wuliu">内地物流</td>');
                        arrayHtml.push('  </tr>');
                        arrayHtml.push('  <tr>');
                        arrayHtml.push('	<td class="the_left">收单件数</td>');
                        if (mllogisItems[i] != undefined) {
                            arrayHtml.push('	<td>' + getVal(mllogisItems[i].ContractQuantity) + '</td>');
                            arrayHtml.push('	<td class="the_left">收单顿重</td>');
                            arrayHtml.push('	<td>' + getVal(mllogisItems[i].ContractWeight) + '</td>');
                            arrayHtml.push('	<td class="the_left">运费/吨</td>');
                            arrayHtml.push('	<td>' + getVal(mllogisItems[i].FreightCharges) + '</td>');
                            arrayHtml.push('	<td class="the_left">保险</td>');
                            arrayHtml.push('	<td>' + getVal(mllogisItems[i].Insurance) + '</td>');
                            arrayHtml.push('	<td class="the_left">运费小计</td>');
                            arrayHtml.push('	<td>' + getVal(mllogisItems[i].SubTotal) + '</td>');
                        }
                        else {
                            arrayHtml.push('	<td></td>');
                            arrayHtml.push('	<td class="the_left">收单顿重</td>');
                            arrayHtml.push('	<td></td>');
                            arrayHtml.push('	<td class="the_left">运费/吨</td>');
                            arrayHtml.push('	<td></td>');
                            arrayHtml.push('	<td class="the_left">保险</td>');
                            arrayHtml.push('	<td></td>');
                            arrayHtml.push('	<td class="the_left">运费小计</td>');
                            arrayHtml.push('	<td></td>');
                        }
                        arrayHtml.push('  </tr>');
                    }
                    //arrayHtml.push('  <tr class="the_fenge">');
                    //arrayHtml.push('	<td colspan="10"></td>');
                    //arrayHtml.push('  </tr>');

                }
            }

            arrayHtml.push('</table>');
            arrayHtml.push('</td>');
            arrayHtml.push('</tr>  ');
        }
        arrayHtml.push('</table>');
        if (this.isSubmit) {
            arrayHtml.push('</form>');
        }
        return arrayHtml.join('');
    }
}


/*销售产品模板行*/
var saleProductDetailFormatterNew = function (rowIndex, rowData) {
    return saleDataFormater.formatOrder(rowIndex, rowData);
}
/*销售交单产品模板行*/
var saleSubmitProductDetailFormatterNew = function (rowIndex, rowData) {
    saleDataFormater.isSubmit = true;
    return saleDataFormater.formatOrder(rowIndex, rowData);
}
//销售模版
var saleDataFormater = {
    //是否交单类型
    isSubmit: false,
    formatOrder: function (rowIndex, rowData) {
        var arrayHtml = new Array();
        //还价信息
        if (!this.isSubmit) {
            arrayHtml.push('<form onsubmit="return submit_sale_bargain_form(this)">');            
        }
        arrayHtml.push('<table class="mobanhang" border="0" cellspacing="1" cellpadding="0">');
        arrayHtml.push('<tr class="the_title">');
        arrayHtml.push('  <td>订单信息</td>');
        arrayHtml.push('</tr>');
        arrayHtml.push('<tr>');
        arrayHtml.push('<td style="padding:0">');
        arrayHtml.push('<table border="0" cellspacing="1" cellpadding="0" style="background-color:#666; width:100%;">');
        arrayHtml.push('  <tr>');
        arrayHtml.push('	<td class="the_left">合同编号</td>');
        arrayHtml.push('	<td>' + rowData.ContractKey + '</td>');
        arrayHtml.push('	<td class="the_left">货品类型</td>');
        arrayHtml.push('	<td>' + func_ContractType_formatter(rowData.OrderType, rowData, rowIndex) + '</td>');
        arrayHtml.push('	<td class="the_left">合同类型</td>');
        arrayHtml.push('	<td>采购合同</td>');
        arrayHtml.push('	<td class="the_left">订单状态</td>');
        arrayHtml.push('	<td>' + func_contractStatus_formatter(rowData.ContractStatus, rowData, rowIndex) + '</td>');
        arrayHtml.push('  </tr>');
        arrayHtml.push('  <tr>');
        arrayHtml.push('	<td class="the_left">订单日期</td>');
        arrayHtml.push('	<td>' + rowData.CTIME_STR + '</td>');
        arrayHtml.push('	<td class="the_left">ETA</td>');
        arrayHtml.push('	<td>' + getVal(rowData.ETA) + '</td>');
        arrayHtml.push('	<td class="the_left">ETD</td>');
        arrayHtml.push('	<td>' + getVal(rowData.ETD) + '</td>');
        arrayHtml.push('	<td class="the_left">操作人</td>');
        arrayHtml.push('	<td>' + getVal(rowData.OperatorPersonName) + '</td>');
        arrayHtml.push('  </tr>');
        arrayHtml.push('  <tr>');
        arrayHtml.push('	<td class="the_left">折扣率</td>');
        arrayHtml.push('	<td>' + getVal(rowData.Payment) + '</td>');
        arrayHtml.push('	<td class="the_left">折扣额</td>');
        arrayHtml.push('	<td>' + getVal(rowData.ShipmentPeriod) + '</td>');
        arrayHtml.push('	<td class="the_left">折扣金额</td>');
        arrayHtml.push('	<td>' + getVal(rowData.ImportDeposite) + '</td>');
        arrayHtml.push('	<td class="the_left">固定订金</td>');
        arrayHtml.push('	<td>' + getVal(rowData.ImportBalancedPayment) + '</td>');
        arrayHtml.push('  </tr>');
        arrayHtml.push('  <tr>');
        arrayHtml.push('	<td class="the_left">汇率</td>');
        arrayHtml.push('	<td>6.25</td>');
        arrayHtml.push('	<td class="the_left">实收金额</td>');
        arrayHtml.push('	<td>' + getVal(rowData.TotalAfterDiscount) + '</td>');
        arrayHtml.push('	<td class="the_left">订单状态</td>');
        arrayHtml.push('	<td></td>');
        arrayHtml.push('	<td class="the_left"></td>');
        arrayHtml.push('	<td></td>');
        arrayHtml.push('  </tr>');
        arrayHtml.push('</table>');
        arrayHtml.push('</td>');
        arrayHtml.push('</tr>');
        arrayHtml.push('<tr class="the_title">');
        arrayHtml.push('<td>商品信息');
        //非交单模板
        if (!this.isSubmit) {
            arrayHtml.push(' <input  type="submit" value="更新还价" class="zdxsj" id="updatePrice' + rowData.ContractKey + '">');
        }
        arrayHtml.push(' </td>');
        arrayHtml.push('</tr>');

        var rows = rowData.ContractType == 0 ? rowData.ContractItems : rowData.SaleProductItems;
        //合同信息
        arrayHtml.push('<input type="hidden" name="SaleContractId" value="' + rowData.ContractId + '"/>');
        if (rows != null && rows != undefined) {
            var productitemrows = rows;
            //香港物流：根据for循环的i去写入
            var hklogisItems = rowData.HongkongLogistics == null || rowData.HongkongLogistics.IsEnable == false ? null : rowData.HongkongLogistics.LogisItems;
            hklogisItems = (hklogisItems == null || hklogisItems == undefined) ? new Array(rows.length) : hklogisItems;
            //内地物流：根据for循环的i去写入
            var mllogisItems = rowData.MainlandLogistics == null || rowData.MainlandLogistics.IsEnable == false ? null : rowData.MainlandLogistics.LogisItems;
            mllogisItems = (mllogisItems == null || mllogisItems == undefined) ? new Array(rows.length) : mllogisItems;
            //var rows = rowData.OrderType == 0 ? rowData.ContractItems : rowData.SaleProductItems;

            arrayHtml.push('<tr>');
            arrayHtml.push('<td style="padding:0">');
            arrayHtml.push('<table border="0" cellspacing="1" cellpadding="0" style="background-color:#666; width:100%;">');

            for (i = 0; i < rows.length; i++) {
                if (productitemrows[i] != null && productitemrows[i].Quantity != null) {
                    arrayHtml.push('  <tr class="the_fenge">');
                    arrayHtml.push('	<td colspan="10"></td>');
                    arrayHtml.push('  </tr>');
                    arrayHtml.push('  <tr>');
                    arrayHtml.push('	<td colspan="10" class="the_wuliu">' + rows[i].ProductName + '</td>');
                    arrayHtml.push('  </tr>');
                    arrayHtml.push('  <tr>');
                    arrayHtml.push('	<td class="the_left">货品出厂编号</td>');
                    arrayHtml.push('	<td>' + rows[i].ProductKey + '</td>');
                    arrayHtml.push('	<td class="the_left">货品名</td>');
                    arrayHtml.push('	<td>' + rows[i].ProductName + '</td>');
                    arrayHtml.push('	<td class="the_left">国家</td>');
                    arrayHtml.push('	<td>');

                    var haveProduct = productitemrows[i].ProductItem && productitemrows[i].ProductItem.Product;
                    if (haveProduct) {
                        arrayHtml.push(getVal(productitemrows[i].ProductItem.Product.MadeInCountry));
                    }
                    arrayHtml.push('    </td>');
                    arrayHtml.push('	<td class="the_left">厂号</td>');
                    arrayHtml.push('	<td>');                   
                    if (haveProduct) {
                        arrayHtml.push(getVal(productitemrows[i].ProductItem.Product.MadeInFactory));
                    }
                    arrayHtml.push('    </td>');
                    arrayHtml.push('	<td class="the_left">品牌</td>');
                    arrayHtml.push('	<td>');
                    if (haveProduct) {
                        arrayHtml.push(getVal(productitemrows[i].ProductItem.Product.Brand));
                    }
                    arrayHtml.push('    </td>');
                    arrayHtml.push('  </tr>');
                    arrayHtml.push('  <tr>');
                    arrayHtml.push('	<td class="the_left">件数</td>');
                    arrayHtml.push('	<td>' + getVal(productitemrows[i].Quantity) + '</td>');
                    arrayHtml.push('	<td class="the_left">净重</td>');
                    arrayHtml.push('	<td>' + getVal(productitemrows[i].NetWeight) + '</td>');
                    arrayHtml.push('	<td class="the_left">单价</td>');
                    arrayHtml.push('	<td>' + getVal(productitemrows[i].UnitPrice) + '</td>');
                    arrayHtml.push('	<td class="the_left">采购币种</td>');
                    arrayHtml.push('	<td>' + getVal(productitemrows[i].Currency) + '</td>');
                    arrayHtml.push('	<td class="the_left">货款小计</td>');
                    arrayHtml.push('	<td>' + getVal(productitemrows[i].SubTotal) + '</td>');
                    arrayHtml.push('  </tr>');
                    arrayHtml.push('  <tr>');
                    arrayHtml.push('	<td class="the_left">还价情况</td>');
                    arrayHtml.push('	<td colspan="10">' + getVal(productitemrows[i].Quantity) + '<span class="blue3">2</span></td>');
                    arrayHtml.push('  </tr>');

                    if (!this.isSubmit) {

                        arrayHtml.push('<tr>');
                        arrayHtml.push('  <td class="the_left">我要还价</td>');
                        arrayHtml.push('  <td>');
                     
                        //商品信息
                        arrayHtml.push('<input type="hidden" name="BargainItems[' + i + '].SaleProductItemId" value="' + getVal(productitemrows[i].ProductItemId) + '" />');
                        //价格信息
                        arrayHtml.push('<input class="gai" style="width:49px;" type="text" name="BargainItems[' + i + '].BargainUnitPrice" value="' + getVal(productitemrows[i].SubTotal) + '"></td>');
                        arrayHtml.push('  <td class="the_left"></td>');
                        arrayHtml.push('  <td></td>');
                        arrayHtml.push('  <td class="the_left"></td>');
                        arrayHtml.push('  <td></td>');
                        arrayHtml.push('  <td class="the_left"></td>');
                        arrayHtml.push('  <td></td>');
                        arrayHtml.push('  <td class="the_left"></td>');
                        arrayHtml.push('  <td></td>');
                        arrayHtml.push(' </tr>');
                    }
                }
            }
            arrayHtml.push('</table>');
            arrayHtml.push('</td>');
            arrayHtml.push('</tr>  ');
        }
        arrayHtml.push('</table>');
        if (!this.isSubmit) {
            arrayHtml.push('</form>');
        }
        return arrayHtml.join('');
    }

}



