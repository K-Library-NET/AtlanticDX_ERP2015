var picked_productkeys_ids = [];

var isOrder = false; //是否采购订单页面

$(document).ready(function () {

    //已选商品列表
    var current_order_product_index = 0;
    $('#picked_products_table tbody').on('click', 'td[class!="operation"][name]', function () {
        var tr = $(this).parent();
        if (tr.find('td:nth-child(2)').text() == '') {
            current_order_product_index = 0;
        } else {
            current_order_product_index = tr.index();
        }
        $('#pick_product_dialog').dialog('open').dialog('setTitle', '选择商品');
    });
    //已选商品列表-添加删除行
    var newOrderedProductsRowHtml = '<tr>' + $('#picked_products_table tbody tr').last().html() + '</tr>';
    $('#picked_products_table tbody').on('click', 'a.add,a.del', function () {
        if ($(this).hasClass('add')) {
            $('#picked_products_table tbody tr').first().before(newOrderedProductsRowHtml);
            $('#picked_products_table tbody tr').first().find('input').val('');

            $('table.related_product_table').each(function (index, obj) {
                $(obj).find('tbody tr').first().before('<tr>' + $(obj).find('tbody tr').first().html() + '</tr>');
                $(obj).find('tbody tr').first().find('input').val('')
            });
        } else if ($(this).hasClass('del')) {
            if ($('#picked_products_table tbody tr').size() == 1) {
                $.messager.alert('提示', '不能删除');
            } else {
                var $tr = $(this).parent().parent();
                var tr_index = $tr.index();
                for (var index in picked_productkeys_ids) {
                    if (picked_productkeys_ids[index] == $tr.find('td').eq(1).text()) {
                        picked_productkeys_ids.splice(index, 1);
                    }
                }
                $tr.remove();

                $('table.related_product_table').each(function (i, obj1) {
                    $(obj1).find('tbody tr').eq(tr_index).remove();
                });

            }
        }

        $('#picked_products_table tbody tr').each(function (i, obj) {
            $(obj).find('td:first').text(i + 1);
            $(obj).find('input[name*="]."],select[name*="]."]').each(function (j, o) {
                var name = $(o).attr("name");
                $(o).attr("name", name.substr(0, name.indexOf('[') + 1) + i + name.substr(name.indexOf(']')));
            });
        });

        $('table.related_product_table').each(function (i, obj1) {
            $(obj1).find('tbody tr').each(function (i, obj) {
                $(obj).find('td:first').text(i + 1);
                $(obj).find('input[name*="]."],select[name*="]."]').each(function (j, o) {
                    var name = $(o).attr("name");
                    $(o).attr("name", name.substr(0, name.indexOf('[') + 1) + i + name.substr(name.indexOf(']')));
                });
            });
        });
    });
    //已选商品列表-总计算
    $('#picked_products_table tbody,table.related_product_table').on('keyup', 'input.num_compute', function () {
        if (isNaN($(this).val())) {
            $(this).val('').focus();
        } else {
            var tr = $(this).parent().parent();
            var type = $(this).prop('name').substr(0, $(this).prop('name').indexOf('['));
            var dataInputs = tr.find('input.num_compute[name^="' + type + '"]');
            if (dataInputs.length == 2 && !isNaN(dataInputs.eq(0).val()) && !isNaN(dataInputs.eq(1).val())) {
                tr.find('td[name="' + type + '.SubTotal"]').text((dataInputs.eq(0).val() * dataInputs.eq(1).val()).toFixed(2));
            }
            var allTotal = compute_all_subtotal();
            $('.compute_all_subtotal').text(allTotal).val(allTotal).data('total', allTotal);
        }
    });



    function compute_all_subtotal() {
        var total = 0;
        $('#picked_products_table td[name$="SubTotal"]').each(function (index, obj) {
            if ($(obj).text() != '' && $.isNumeric($(obj).text())) {
                total += parseFloat($(obj).text());
            }
        });
        return total.toFixed(2);
    }

    //选择商品查询
    var PICK_PRODUCT_DIALOG_FILTERVALUE = '请输入商品编号或名称或规格';
    $('#pick_product_dialog input[name="filterValue"]').focus(function () {
        if ($(this).val() == PICK_PRODUCT_DIALOG_FILTERVALUE) {
            $(this).val('');
        }
    }).blur(function () {
        if ($(this).val() == '') {
            $(this).val(PICK_PRODUCT_DIALOG_FILTERVALUE);
        }
    }).keydown(function (e) {
        if (e.keyCode == 13) {
            $('#btn_order_products_filter').click();
        }
    });
    $('#btn_pick_products_filter').click(function () {
        var filterValue = $('#pick_product_dialog input[name="filterValue"]').val();
        if (filterValue == PICK_PRODUCT_DIALOG_FILTERVALUE) {

        } else {
            $('#pick_products_datagrid').datagrid('load', {
                filterValue: filterValue
            });
        }
    });
    //选中商品
    var pick_product_id_name = $('#pick_product_dialog_bottons a:first').data('id-name');
    $('#pick_product_dialog_bottons a:first').click(function () {
        var row = $('#pick_products_datagrid').datagrid('getSelected');
        if (row == null) {
            $.messager.alert('提示', '请选择商品');
        } else {
            if (picked_productkeys_ids.contains(row[pick_product_id_name])) {
                $.messager.alert('提示', '该商品已选择');
            } else {
                var tr = $('#picked_products_table tbody tr:nth-child(' + (current_order_product_index + 1) + ')');
                for (var key in row) {
                    if ($.type(row[key]) == 'object') {
                        for (var key2 in row[key]) {
                            if ($.type(row[key][key2]) == 'object') {
                                for (var key3 in row[key][key2]) {
                                    if ($.type(row[key][key2][key3]) != 'object') {
                                        tr.find('td[name="' + key + '.' + key2 + '.' + key3 + '"]').text(row[key][key2][key3]);
                                    }
                                }
                            } else {
                                tr.find('td[name="' + key + '.' + key2 + '"]').text(row[key][key2]);
                            }
                        }
                    } else {
                        tr.find('td[name="' + key + '"]').text(row[key]);
                    }
                }
                tr.find('input[type!="hidden"]').val('');//清掉已经填写数据
                tr.find('input[name$="ProductKey"]').val(row['ProductKey']);
                tr.find('input[name$="ProductId"]').val(row['ProductId']);
                tr.find('input[name$="' + pick_product_id_name + '"]').val(row[pick_product_id_name]);
                tr.find('td[name$="SubTotal"]').text('');
                tr.find('select').each(function (index, obj) {
                    $(obj).val($(obj).find('option:first').val());
                });

                $('table.related_product_table').each(function (i, obj1) {
                    $(obj1).find('tbody tr:nth-child(' + (current_order_product_index + 1) + ') td[name="ProductFullName"]').text(row['ProductFullName']);
                    if (row['ProductId'] != undefined) {
                        $(obj1).find('tbody tr:nth-child(' + (current_order_product_index + 1) + ') input[name$="ProductId"]').val(row['ProductId']);
                    }
                    if (row['ProductItemId'] != undefined) {
                        $(obj1).find('tbody tr:nth-child(' + (current_order_product_index + 1) + ') input[name$="ProductId"]').val(row['ProductItemId']);
                    }
                    if (row['StockItemId'] != undefined) {
                        $(obj1).find('tbody tr:nth-child(' + (current_order_product_index + 1) + ') input[name$="StockItemId"]').val(row['StockItemId']);
                    }
                });

                picked_productkeys_ids.push(row[pick_product_id_name]);
                $('#pick_product_dialog').dialog('close');
                //如果是采购订单则添加相应的物流信息
                if (isOrder) {
                    var i = picked_productkeys_ids.length;
                    var htmlArray = new Array();
                    //添加香港物流信息tb_HongKong
                    htmlArray.push('<tr> ');
                    htmlArray.push('<td class="check">' + i + '</td>');
                    htmlArray.push(' <td>');
                    htmlArray.push(row['ProductFullName']);
                    htmlArray.push('<input type="hidden" name="HongkongLogistics.LogisItems[' + i + ']" ');
                    htmlArray.push(' value="' + row['ProductId'] + '" />');
                    htmlArray.push(' </td>');
                    htmlArray.push('<td>' + row['MadeInCountry'] + '</td>');
                    htmlArray.push('<td>' + row['MadeInFactory'] + '</td>');
                    htmlArray.push('<td>');
                    htmlArray.push('<input name="HongkongLogistics.LogisItems[' + i + '].ContractQuantity" type="text" class="HongkongLogistics quantity" value="' + getVal(row['Quantity'], 0) + '" />');
                    htmlArray.push(' </td>');
                    htmlArray.push(' <td>');
                    htmlArray.push(' <input name="HongkongLogistics.LogisItems[' + i + '].ContractWeight" type="text" class="num_compute" />');
                    htmlArray.push(' </td>');
                    htmlArray.push(' <td>');
                    htmlArray.push(' <input name="HongkongLogistics.LogisItems[' + i + '].FreightCharges" type="text" class="num_compute" />');
                    htmlArray.push(' </td>');
                    htmlArray.push('  <td>');
                    htmlArray.push('  <input name="HongkongLogistics.LogisItems[' + i + '].Insurance" type="text" class="num_compute" />');
                    htmlArray.push('</td>');
                    htmlArray.push('<td>');
                    htmlArray.push('<input name="HongkongLogistics.LogisItems[' + i + '].SubTotal" type="text" readonly="readonly" />');
                    htmlArray.push('</td>');
                    htmlArray.push('</tr>');
                    $("#tb_HongKong").prepend(htmlArray.join(''));
                    //添加内地物流信息tb_MainLand   
                    htmlArray = new Array();
                    htmlArray.push('<tr> ');
                    htmlArray.push('<td class="check">' + i + '</td>');
                    htmlArray.push(' <td>');
                    htmlArray.push(row['ProductFullName']);
                    htmlArray.push('<input type="hidden" name="MainlandLogistics.LogisItems[' + i + ']" ');
                    htmlArray.push(' value="' + row['ProductId'] + '" />');
                    htmlArray.push(' </td>');
                    htmlArray.push('<td>' + row['MadeInCountry'] + '</td>');
                    htmlArray.push('<td>' + row['MadeInFactory'] + '</td>');
                    htmlArray.push('<td>');
                    htmlArray.push('<input name="MainlandLogistics.LogisItems[' + i + '].ContractQuantity" type="text" class="MainlandLogistics quantity"  value="' + getVal(row['Quantity'], 0) + '" />');
                    htmlArray.push(' </td>');
                    htmlArray.push(' <td>');
                    htmlArray.push(' <input name="MainlandLogistics.LogisItems[' + i + '].ContractWeight" type="text" class="num_compute" />');
                    htmlArray.push(' </td>');
                    htmlArray.push(' <td>');
                    htmlArray.push(' <input name="MainlandLogistics.LogisItems[' + i + '].FreightCharges" type="text" class="num_compute" />');
                    htmlArray.push(' </td>');
                    htmlArray.push('  <td>');
                    htmlArray.push('  <input name="MainlandLogistics.LogisItems[' + i + '].Insurance" type="text" class="num_compute" />');
                    htmlArray.push('</td>');
                    htmlArray.push('<td>');
                    htmlArray.push('<input name="MainlandLogistics.LogisItems[' + i + '].SubTotal" type="text" readonly="readonly" />');
                    htmlArray.push('</td>');
                    htmlArray.push('</tr>');
                    $("#tb_MainLand").prepend(htmlArray.join(''));

                }
            }
        }
    });

    //商品件数
    $('#picked_products_table tbody,table.related_product_table').on('keyup','.quantity', function () {
        if (isNaN(parseInt($(this).val()))) {
            $(this).val('').focus();
        } else {
            // 如果是采购订单(更新对应物流信息)
            if (isOrder) {
                var name = $(this).attr("name");
                //订单商品信息
                if (name.indexOf('ContractItems') == 0) {
                    var index = $('.quantity').index(this);
                    var count = $(this).val();
                    $('.quantity').eq($('.quantity').length / 3 + index).val(count);
                    $('.quantity').eq($('.quantity').length *2/ 3 + index).val(count);
                }              

            }
        }
    });

});