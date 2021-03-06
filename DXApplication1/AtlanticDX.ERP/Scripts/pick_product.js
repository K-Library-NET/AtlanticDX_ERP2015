﻿var picked_productkeys_ids = [];

var isOrder = false; //是否采购订单页面

$(document).ready(function () {

    //已选商品列表
    var current_order_product_index = 0;
    $('#picked_products_table tbody').on('click', 'td[class!="operation"][name]', function () {
        var tr = $(this).parent();
        current_order_product_index = tr.index();
        $('#pick_product_dialog').dialog('open').dialog('setTitle', '选择商品');
    });
    //已选商品列表-添加删除行
    var newOrderedProductsRowHtml = '<tr>' + $('#picked_products_table tbody tr').last().html() + '</tr>';
    var newHhRowHtml = $('#tb_HongKong tr').last().parent().html();
    var newMlRowHtml = $('#tb_MainLand tr').last().parent().html();
    $('#picked_products_table tbody').on('click', 'a.add,a.del', function () {
        if ($(this).hasClass('add')) {
            $('#picked_products_table tbody tr').last().after(newOrderedProductsRowHtml);
            //清空编辑合同时新增商品
            $('#picked_products_table tbody tr').last().find('td').each(function (k) {
                if ($(this).attr('name') != undefined) {
                    $(this).text('');
                }
            });
            $('#picked_products_table tbody tr').last().find('input').val('');

            $('table.related_product_table').each(function (index, obj) {
                $(obj).find('tbody tr').last().after('<tr>' + $(obj).find('tbody tr').last().html() + '</tr>');
                $(obj).find('tbody tr').last().find('input').val('')
            });
            //如果是采购订单则添加相应的物流信息
            if (isOrder) {
                //香港物流
                $('#tb_HongKong tr').last().after(newHhRowHtml);
                $('#tb_HongKong tr').each(function (i, obj) {
                    $(obj).find('td:first').text(i + 1);
                });
                $('#tb_HongKong  tr').last().find('td').each(function (k) {
                    if ($(this).attr('name') != undefined) {
                        $(this).text('');
                    }
                });
                $('#tb_MainLand tr').last().find('input').val('');
                //内地物流
                $('#tb_MainLand tr').last().after(newMlRowHtml);
                $('#tb_MainLand tr').each(function (i, obj) {
                    $(obj).find('td:first').text(i + 1);
                });
                $('#tb_MainLand  tr').last().find('td').each(function (k) {
                    if ($(this).attr('name') != undefined) {
                        $(this).text('');
                    }
                });
                $('#tb_HongKong tr').last().find('input').val('');
            }
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

                //如果是采购订单则添加相应的物流信息
                if (isOrder) {
                    //香港物流
                    $('#tb_HongKong tr').eq(tr_index).remove();                   
                    //内地物流
                    $('#tb_MainLand tr').eq(tr_index).remove();
                }

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
                //单价统一按照“千克”计算
                tr.find('td[name="' + type + '.SubTotal"]').text((1*dataInputs.eq(0).val() * dataInputs.eq(1).val()).toFixed(2));
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
                setTrContent(tr, row, pick_product_id_name);
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
                    //香港物流
                    var trH = $('#tb_HongKong tr:nth-child(' + (current_order_product_index + 1) + ')');
                    setTrContent(trH, row, pick_product_id_name);
                    //内地物流
                    var trM = $('#tb_MainLand tr:nth-child(' + (current_order_product_index + 1) + ')');
                    setTrContent(trM, row, pick_product_id_name);                  

                }
            }
        }
    });

    //商品件数
    $('#picked_products_table tbody,table.related_product_table').on('keyup', '.quantity', function () {
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
                    $('.quantity').eq($('.quantity').length * 2 / 3 + index).val(count);
                }

            }
        }
    });

});

//设置行信息
/*
tr:显示行
row:数据源
pick_product_id_name:商品信息
*/
function setTrContent(tr, row, pick_product_id_name) {
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
    tr.find('input[name$="ProductItemId"]').val(row['ProductItemId']);
    tr.find('input[name$="' + pick_product_id_name + '"]').val(row[pick_product_id_name]);
    tr.find('td[name$="SubTotal"]').text('');
    tr.find('select').each(function (index, obj) {
        $(obj).val($(obj).find('option:first').val());
    });
}