//datagrid增删改操作
var add_action = function () {
    $('#add_dialog').dialog('open').dialog('setTitle', '添加');
};

var add_row = function () {
    add_action();
};

var add_handler = function () {
    add_action();
};

var edit_action = function (row) {
    $('#edit_dialog').dialog('open').dialog('setTitle', '编辑');
    $('#edit_dialog form').form('load', row);
}

var edit_row = function (index) {
    var row = $('#my_datagrid').datagrid('getRows')[index];
    edit_action(row);
}


var edit_handler = function () {
    var row = $('#my_datagrid').datagrid('getSelected');
    if (row) {
        edit_action(row);
    } else {
        $.messager.alert('提示', '请选择一行数据');
    }
};


var remove_action = function (row) {
    $.messager.confirm('确认', '确定删除吗?', function (r) {
        if (r) {
            var queryString = '1=1';
            for (var key in row) {
                if (!/^\/Date\([0-9]+\)\/$/.test(row[key])) {
                    queryString += '&' + key + '=' + row[key];
                }
            }
            $.ajax({
                url: '/' + Area + '/' + Controller + '/Remove',
                type: 'post',
                data: queryString,
                dataType: 'json',
                beforeSend: function () {
                    $.messager.progress({
                        title: '',
                        msg: '处理中...'
                    });
                },
                success: function (resp) {
                    $.messager.progress('close');
                    var messages = [];
                    for (i = 0; i < resp.length; i++) {
                        for (j = 0; j < resp[i]['Messages'].length; j++) {
                            messages.push(resp[i]['Messages'][j]);
                        }
                    }
                    if (messages.length > 0) {
                        $.messager.alert('提示', messages.join('\n'));
                    } else {
                        $.messager.alert('提示', '删除成功');
                        $('#my_datagrid').datagrid('reload');
                    }
                }
            });
        }
    });
}

var remove_row = function (index) {
    var row = $('#my_datagrid').datagrid('getRows')[index];
    remove_action(row);
}

var remove_handler = function () {
    var row = $('#my_datagrid').datagrid('getSelected');
    if (row) {
        remove_action(row);
    } else {
        $.messager.alert('提示', '请选择一行数据');
    }
};

var datagrid_toolbar = [{
    text: '添加',
    iconCls: 'icon-add',
    handler: add_handler
}, {
    text: '编辑',
    //iconCls: 'icon-edit',
    handler: edit_handler
}, {
    text: '删除',
    //iconCls: 'icon-remove',
    handler: remove_handler
}];

var get_datagrid_row_by_index = function (index) {
    var row = $('#my_datagrid').datagrid('getRows')[index];
    return row;
}

//close dialog
var closeDialog = function (selector) {
    $(selector).dialog('close')
}

//submit form
function submitForm(selector) {
    if ($(selector).form('validate')) {
        $(selector).submit();
    }
}

$.fn.serializeJson = function () {
    var arr = this.serializeArray();
    var json = {};
    for (i = 0; i < arr.length; i++) {
        if (json[arr[i].name] != undefined) {
            json[arr[i].name] = json[arr[i].name] + ',' + arr[i].value;
        } else {
            json[arr[i].name] = arr[i].value;
        }
    }
    return json;
}

function get_current_date() {
    var date = new Date();
    return date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
}

$(function () {
    jQuery.validator.setDefaults({
        debug: false,
        ignore: '',
        onkeyup: false,
        //onfocusout: false,
        showErrors: function (errorMap, errorList) {
            if (errorList != null && errorList.length > 0) {
                $.messager.alert('提示', errorList[0].message);
            }
        }
    });

    //datagrid选择行事件
    $('#my_datagrid').datagrid({
        onSelect: function (index, row) {
            $('#my_datagrid').datagrid('unselectRow', index);
        }
    });

    //add dialog
    $('#add_dialog').dialog({
        modal: true,
        onClose: function () {
            $('#my_datagrid').datagrid('unselectAll');
            $('#add_dialog form').form('reset');
        }
    });

    //submit add form
    var addValidateOptions = get_validate_rules_messages('#add_dialog form');
    $('#add_dialog form').validate({
        rules: addValidateOptions.rules,
        messages: addValidateOptions.messages,
        submitHandler: function (form) {
            $.ajax({
                url: '/' + Area + '/' + Controller + '/Add',
                type: 'post',
                data: $('#add_dialog form').serializeJson(),
                dataType: 'json',
                beforeSend: function () {
                    $.messager.progress({
                        title: '',
                        msg: '处理中...'
                    });
                },
                success: function (resp) {
                    $.messager.progress('close');
                    var messages = GetModelStateErrors(resp);
                    if (messages.length > 0) {
                        $.messager.alert('提示', messages.join('<br/>'));
                    } else {
                        $.messager.alert('提示', '操作成功');
                        $('#my_datagrid').datagrid('reload');
                        $('#add_dialog').dialog('close');
                    }
                }
            });
            return false;
        }
    });

    //edit dialog
    $('#edit_dialog').dialog({
        modal: true,
        onClose: function () {
            $('#my_datagrid').datagrid('unselectAll');
            $('#edit_dialog form').form('reset');
        }
    });

    //submit edit form
    var editValidateOptions = get_validate_rules_messages('#edit_dialog form');
    $('#edit_dialog form').validate({
        rules: editValidateOptions.rules,
        messages: editValidateOptions.messages,
        submitHandler: function (form) {
            $.ajax({
                url: '/' + Area + '/' + Controller + '/Edit',
                type: 'post',
                data: $('#edit_dialog form').serializeJson(),
                dataType: 'json',
                beforeSend: function () {
                    $.messager.progress({
                        title: '',
                        msg: '处理中...'
                    });
                },
                success: function (resp) {
                    $.messager.progress('close');
                    var messages = GetModelStateErrors(resp);
                    if (messages.length > 0) {
                        $.messager.alert('提示', messages.join('<br/>'));
                    } else {
                        $.messager.alert('提示', '编辑成功');
                        $('#my_datagrid').datagrid('reload');
                        $('#edit_dialog').dialog('close');
                    }
                }
            });
            return false;
        }
    });

    //fiter form
    var filterValidateOptions = get_validate_rules_messages('#filter_form');
    $('#filter_form').validate({
        rules: filterValidateOptions.rules,
        messages: filterValidateOptions.messages,
        submitHandler: function (form) {
            $('#filter_dialog').hide();
            var filters = {};
            var arr = $('#filter_form').serializeArray();
            for (i = 0; i < arr.length; i++) {
                filters[arr[i]['name']] = arr[i]['value'];
            }
            $('#my_datagrid').datagrid('load', filters);
            return false;
        }
    });

});

/*检查一个array是否包含某个值
@return bool
*/
Array.prototype.contains = function (obj) {
    var i = this.length;
    while (i--) {
        if (this[i] === obj) {
            return true;
        }
    }
    return false;
}

/*获取服务端输出的ModelState错误
@return array
*/
function GetModelStateErrors(resp) {
    var messages = [];
    for (i = 0; i < resp.length; i++) {
        for (j = 0; j < resp[i]['Messages'].length; j++) {
            messages.push(resp[i]['Messages'][j]);
        }
    }
    return messages;
}

/*将MVC框架生成的验证信息字段转换成jquery.validate验证*/
function get_validate_rules_messages(form) {
    var rule_names = ['required', 'date', 'number', 'min', 'max'];
    var rules = {};
    var messages = {};
    $('input[data-val-required],input[data-val-date],input[data-val-number],input[data-val-min],input[data-val-max]', $(form)).each(function (j, obj) {
        var name = $(obj).hasClass('easyui-datebox') ? $(obj).attr('textboxname') : $(obj).prop('name');
        if (name != undefined) {
            rules[name] = {};
            messages[name] = {};
            for (i = 0; i < rule_names.length; i++) {
                var msg = $(obj).attr('data-val-' + rule_names[i]);
                if (msg != undefined) {
                    if (rule_names[i] == 'min' || rule_names[i] == 'max') {
                        if ($.isNumeric(msg)) {
                            rules[name][rule_names[i]] = parseInt(msg);
                        }
                    } else {
                        rules[name][rule_names[i]] = true;
                        if (msg != '') {
                            messages[name][rule_names[i]] = msg;
                        }
                    }
                }
            }
        }
    });
    return { rules: rules, messages: messages };

};

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
    } else if (value == 2) {
        temp = '审核通过';
    } else if (value == 1) {
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


/*采购产品模板行*/
var productDetailFormatterNew = function (rowIndex, rowData) {
    var arrayHtml = new Array();
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
    arrayHtml.push('<td>商品信息</td>');
    arrayHtml.push('</tr>');

    var rows = rowData.ContractType == 0 ? rowData.ContractItems : rowData.SaleProductItems;
    var productitemrows = rowData.ContractItems;
    //香港物流：根据for循环的i去写入
    var hklogisItems = rowData.HongkongLogistics == null || rowData.HongkongLogistics.IsEnable == false ?null : rowData.HongkongLogistics.LogisItems;
    hklogisItems = (hklogisItems == null || hklogisItems == undefined )? new Array(rows.length) : hklogisItems;
    //内地物流：根据for循环的i去写入
    var mllogisItems = rowData.MainlandLogistics == null || rowData.MainlandLogistics.IsEnable == false ? null : rowData.MainlandLogistics.LogisItems;
    mllogisItems = (mllogisItems == null || mllogisItems == undefined) ? new Array(rows.length) : mllogisItems;
    //var rows = rowData.OrderType == 0 ? rowData.ContractItems : rowData.SaleProductItems;
    if (rows == null) return '';
    for (i = 0; i < rows.length; i++) {
        if (productitemrows[i] != null && productitemrows[i].Quantity != null) {
            arrayHtml.push('<tr>');
            arrayHtml.push('<td style="padding:0">');
            arrayHtml.push('<table border="0" cellspacing="1" cellpadding="0" style="background-color:#666; width:100%;">');
            arrayHtml.push('  <tr class="the_fenge">');
            arrayHtml.push('	<td colspan="10"></td>');
            arrayHtml.push('  </tr>');
            arrayHtml.push('  <tr>');
            arrayHtml.push('	<td colspan="10" class="the_wuliu">商品</td>');
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
                arrayHtml.push('	<td>');
                arrayHtml.push('	<td>' + getVal(hklogisItems[i].FreightCharges) + '</td>');
                arrayHtml.push('	<td class="the_left">保险</td>');
                arrayHtml.push('	<td>');
                arrayHtml.push('	<td>' + getVal(hklogisItems[i].Insurance) + '</td>');
                arrayHtml.push('	<td class="the_left">运费小计</td>');
                arrayHtml.push('	<td>');
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
            arrayHtml.push('  <tr class="the_fenge">');
            arrayHtml.push('	<td colspan="10"></td>');
            arrayHtml.push('  </tr>');

        }
    }

    arrayHtml.push('</table>');
    arrayHtml.push('</td>');
    arrayHtml.push('</tr>  ');
    arrayHtml.push('</table>');
    return arrayHtml.join('');
}